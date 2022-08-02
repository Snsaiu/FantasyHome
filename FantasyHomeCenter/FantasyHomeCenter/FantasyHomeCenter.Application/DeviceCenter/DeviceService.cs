
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.RoomCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.Core.Enums;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;

using Furion;
using Furion.DatabaseAccessor;
using Furion.TaskScheduler;

using Mapster;
using Microsoft.EntityFrameworkCore;


namespace FantasyHomeCenter.Application.DeviceCenter;

public class DeviceService:IDynamicApiController,ITransient,IDeviceService
{
    private readonly IRepository<Device> repository;
    private readonly IRepository<DeviceType> deviceTypeRepository;
    private readonly IRepository<Room> roomRepository;
    private readonly IPluginService pluginService;
    private readonly IRepository<CommandConstParams> commandConstParamsRepository;
    private readonly IDeviceTypeService deviceTypeService;
    private readonly PluginStateChangeNotification pluginStateChangeNotification;


    public DeviceService(IRepository<Device> repository,
        IRepository<DeviceType> deviceTypeRepository,
        IRepository<Room> roomRepository,
        IPluginService pluginService,
        IRepository<CommandConstParams> commandConstParamsRepository,
        IDeviceTypeService deviceTypeService,
        PluginStateChangeNotification pluginStateChangeNotification
       )
    {
        this.repository = repository;
        this.deviceTypeRepository = deviceTypeRepository;
        this.roomRepository = roomRepository;
        this.pluginService = pluginService;
        this.commandConstParamsRepository = commandConstParamsRepository;
        this.deviceTypeService = deviceTypeService;
        this.pluginStateChangeNotification = pluginStateChangeNotification;
    }
    public async Task<RESTfulResult< PagedList<DeviceOutput>>> GetDevices(DeviceInput input)
    {
        var where = this.repository.AsQueryable();
        if (input.deviceType != null)
        {
            where.Where(x => x.Type.Id == input.deviceType);
        }
        var res= await  where.Select(x => new DeviceOutput()
            {
                Address = x.Address,
                Id = x.Id, 
                Name = x.Name,
                Type = x.Type.DeviceTypeName,
                Description =x.Description, 
                DeviceTypeId = x.Type.Id,
                State = x.State,
                RoomName = x.Room.RoomName
                
            })
            .ToPagedListAsync(input.PageIndex,input.PageSize);
        return new RESTfulResult<PagedList<DeviceOutput>>() { Succeeded = true, Data = res };
    }

    public async Task<RESTfulResult<DeviceTypesAndRoomsOutput>> GetDeviceTypesAndRoomsAsync()
    {
        DeviceTypesAndRoomsOutput res = new DeviceTypesAndRoomsOutput();
       var rooms=await  this.roomRepository.Entities.ToListAsync();
       res.Rooms = rooms.Adapt<List<RoomOutput>>();
       var types = await this.deviceTypeRepository.Entities.ToListAsync();
       res.DeviceTypes = types.Adapt<List<DeviceTypeOutput>>();
       return new RESTfulResult<DeviceTypesAndRoomsOutput>() { Succeeded = true, Data = res };

    }

    public async Task<RESTfulResult<int>> AddDeviceAsync(AddDeviceInput input)
    {
        Room room =await this.roomRepository.Where(x => x.Id == input.RoomId).FirstOrDefaultAsync();
        DeviceType type = await this.deviceTypeRepository.Where(predicate: x => x.Id == input.DeviceTypeId).FirstOrDefaultAsync();

        Device entity= input.Adapt<Device>();
        entity.Room = room;
        entity.Type = type;
        foreach (var item in input.InitCommandParams)
        {
            CommandConstParams cp = new CommandConstParams();
            cp.Device = entity;
            cp.Name = item.Key;
            cp.Value = item.Value;
            cp.Type = CommandParameterType.Init;
            entity.ConstCommandParams.Add(cp);
        }
        foreach (KeyValue<string, string> item in input.SetCommandParams)
        {
            CommandConstParams cp = new CommandConstParams();
            cp.Device = entity;
            cp.Name = item.Key;
            cp.Value = item.Value;
            cp.Type = CommandParameterType.Set;
            entity.ConstCommandParams.Add(cp);
        }
        foreach (KeyValue<string, string> item in input.GetCommandParams)
        {
            CommandConstParams cp = new CommandConstParams();
            cp.Device = entity;
            cp.Name = item.Key;
            cp.Value = item.Value;
            cp.Type = CommandParameterType.Get;
            entity.ConstCommandParams.Add(cp);
        }

        var deviceControllRes= await this.deviceTypeService.GetDeviceControllerById(input.DeviceTypeId);

       

        if (!deviceControllRes.Succeeded)
        {
            return new RESTfulResult<int> { Succeeded = false, Errors = deviceControllRes.Errors };
        }
        
        var deviceType =  deviceTypeRepository.AsQueryable().First(x => x.Id == input.DeviceTypeId);


        var controller = deviceControllRes.Data;
        if( controller.BackgroundParam!=null)
        {
            SpareTime.Do(controller.BackgroundParam.Time, async (time, count) =>
            {
                System.Console.WriteLine(input.Name + "___" + controller.BackgroundParam.TaskName+"::任务执行"+count+"次");
                var getlist= entity.ConstCommandParams.Where(x => x.Type == CommandParameterType.Get).ToList();
                List<DeviceInputParameter> inputs = new List<DeviceInputParameter>();

                if(getlist!=null)
                {
                    foreach (var item in getlist)
                    {
                        inputs.Add(new DeviceInputParameter(item.Name, item.Value));
                    }
                }

               var getRes= await controller.GetDeviceStateAsync(inputs, deviceType.PluginPath);
                if(getRes.Success)
                {
                    MqttSendInfo info = new MqttSendInfo();
                    info.Data = getRes.Data;
                    
                    info.DeviceName=input.Name;
                    info.Success = true;
                    info.PluginKey = deviceType.Key;
                    //info.Topic=deviceType.Key;
                    //mqtt 发送
                   await App.GetService<MqttServerInstance>().PublishAsync(deviceType.Key,info);
                }
                else
                {
                    System.Console.WriteLine(input.Name + "___" + controller.BackgroundParam.TaskName + "::任务执行发生错误"+getRes.ErrorMessage);
                }

            }, input.Name + "___" + controller.BackgroundParam.TaskName, controller.BackgroundParam.Description);
        }

        //通知设备重启
        await App.GetService<MqttServerInstance>().PublishAsync("fantasyhome-restart", new MqttSendInfo());



        var entityRes= await  this.repository.InsertNowAsync(entity);


         return new RESTfulResult<int>() { Succeeded = true };
      
    }

    public async Task<RESTfulResult<Dictionary<string, string>>> GetGetDeviceCommandParamsbyDeviceId(int id)
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        var entities =await this.commandConstParamsRepository.Include(x => x.Device).Where(x=>x.Device.Id==id&&x.Type==CommandParameterType.Get).ToListAsync();

       foreach (CommandConstParams item in entities)
       {
           res.Add(item.Name,item.Value);
       }
       return await Task.FromResult<RESTfulResult<Dictionary<string, string>>>(new RESTfulResult<Dictionary<string, string>>()
       {
           Succeeded = true,
           Data = res
       });
    }

    public async Task<RESTfulResult<Dictionary<string, string>>> GetSetDeviceCommandParamsByDeviceId(int id)
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        var entities =await this.commandConstParamsRepository.Include(x => x.Device).Where(x=>x.Device.Id==id&&x.Type==CommandParameterType.Set).ToListAsync();

        foreach (CommandConstParams item in entities)
        {
            res.Add(item.Name,item.Value);
        }
        return await Task.FromResult<RESTfulResult<Dictionary<string, string>>>(new RESTfulResult<Dictionary<string, string>>()
        {
            Succeeded = true,
            Data = res
        });
    }

    
    public RESTfulResult<Dictionary<string, string>> GetDeviceState(MqttSendInfo info)
    {
      // 找到插件

      var pluginType = this.deviceTypeRepository.AsEnumerable().FirstOrDefault(x => x.Key == info.PluginKey);
      if (pluginType==null)
      {
          return new RESTfulResult<Dictionary<string, string>>() { Succeeded = false, Errors = "没有找到实体插件路径" };
      }
      
      var plugin = this.pluginService.GetPluginByKeyAsync(info.PluginKey).GetAwaiter().GetResult();
      
      if (plugin.Succeeded)
      {
          List<DeviceInputParameter> param = new();

          foreach (var item in info.Data)
          {
              DeviceInputParameter p = new(item.Key,item.Value);
              param.Add(p);
          }

         var commandRes= plugin.Data.GetDeviceStateAsync(param, pluginType.PluginPath).GetAwaiter().GetResult();
         if (commandRes.Success)
         {
             return new RESTfulResult<Dictionary<string, string>>()
             {
                 Succeeded = true, Data = commandRes.Data

             };
         }
         else
         {
             return new RESTfulResult<Dictionary<string, string>>() { Succeeded = false, Errors = "获得插件数据失败" };
         }
      }
      else
      {
          return new RESTfulResult<Dictionary<string, string>>() { Succeeded = false, Errors = "没有找到插件" };
      }



    }

    public RESTfulResult<Dictionary<string, string>> SetThenGetDeviceState(MqttSendInfo info)
    {
        var pluginType = this.deviceTypeRepository.AsEnumerable().FirstOrDefault(x => x.Key == info.PluginKey);
        if (pluginType==null)
        {
            return new RESTfulResult<Dictionary<string, string>>() { Succeeded = false, Errors = "没有找到实体插件路径" };
        }
      
        //var plugin = this.pluginService.GetPluginByKeyAsync(info.PluginKey).GetAwaiter().GetResult();

        var plugin = this.pluginStateChangeNotification.GetDevices().First(x => x.Key == info.PluginKey);

        if (plugin!=null)
        {
            List<DeviceInputParameter> param = new();

            foreach (var item in info.Data)
            {
                DeviceInputParameter p = new(item.Key,item.Value);
                param.Add(p);
            }

            var commandRes= plugin.SetDeviceStateWithNotifyAsync(info.DeviceName,param, pluginType.PluginPath).GetAwaiter().GetResult();
            if (commandRes.Success)
            {
                return new RESTfulResult<Dictionary<string, string>>()
                {
                    Succeeded = true, Data = commandRes.Data

                };
            }
            else
            {
                return new RESTfulResult<Dictionary<string, string>>() { Succeeded = false, Errors = "获得插件数据失败" };
            }
        }
        else
        {
            return new RESTfulResult<Dictionary<string, string>>() { Succeeded = false, Errors = "没有找到插件" };
        }
    }

    public  RESTfulResult<List<DeviceOutput>> GetAllDevices()
    {
        var where = this.repository.AsQueryable();

        var res =  where.Select(x => new DeviceOutput()
        {
            Address = x.Address,
            Id = x.Id,
            Name = x.Name,
            Type = x.Type.DeviceTypeName,
            Description = x.Description,
            DeviceTypeId = x.Type.Id,
            State = x.State,
            RoomName = x.Room.RoomName

        }).ToList();
            
        return new RESTfulResult<List<DeviceOutput>>() { Succeeded = true, Data = res };
    }

    public RESTfulResult<List<PropertyModel>> GetDeviceControllPropertiesByDeviceTypeId(int deviceTypeId)
    {
        var plugin= this.deviceTypeRepository.AsEnumerable().First(x => x.Id == deviceTypeId);

       var controller=  this.pluginStateChangeNotification.GetDevices().Where(x => x.Key == plugin.Key).First();

        return new RESTfulResult<List<PropertyModel>> { Data = controller.GetDeviceProperties(), Succeeded = true };
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Application.RoomCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.Core.Enums;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;
using Furion.DatabaseAccessor;
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


    public DeviceService(IRepository<Device> repository,
        IRepository<DeviceType> deviceTypeRepository,
        IRepository<Room> roomRepository,
        IPluginService pluginService,
        IRepository<CommandConstParams> commandConstParamsRepository)
    {
        this.repository = repository;
        this.deviceTypeRepository = deviceTypeRepository;
        this.roomRepository = roomRepository;
        this.pluginService = pluginService;
        this.commandConstParamsRepository = commandConstParamsRepository;
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
        DeviceType type = await this.deviceTypeRepository.Where(x => x.Id == input.DeviceTypeId).FirstOrDefaultAsync();

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
        foreach (var item in input.SetCommandParams)
        {
            CommandConstParams cp = new CommandConstParams();
            cp.Device = entity;
            cp.Name = item.Key;
            cp.Value = item.Value;
            cp.Type = CommandParameterType.Set;
            entity.ConstCommandParams.Add(cp);
        }
        foreach (var item in input.GetCommandParams)
        {
            CommandConstParams cp = new CommandConstParams();
            cp.Device = entity;
            cp.Name = item.Key;
            cp.Value = item.Value;
            cp.Type = CommandParameterType.Get;
            entity.ConstCommandParams.Add(cp);
        }
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
}
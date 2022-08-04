using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.DevicePluginInterface;

using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Furion.TaskScheduler;

using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter;

public class BackgroundTaskService:IBackgroundTaskService,IDynamicApiController,ITransient
{
    private readonly IRepository<Automation> _automationRepository;
    private readonly IRepository<Device> deviceServiRepository;
    private readonly IRepository<DeviceType> deviceTypeRepository;
    private readonly PluginStateChangeNotification pluginStateChangeNotification;

    private readonly IDeviceService deviceService;

    public BackgroundTaskService(
        IRepository<Automation> automationRepository,
        IRepository<Device> deviceServiRepository,
        IRepository<DeviceType> deviceTypeRepository,
        PluginStateChangeNotification pluginStateChangeNotification,IDeviceService deviceService)
    {
        _automationRepository = automationRepository;
        this.deviceServiRepository = deviceServiRepository;
        this.deviceTypeRepository = deviceTypeRepository;
        this.pluginStateChangeNotification = pluginStateChangeNotification;
      
        this.deviceService = deviceService;
    }

    public RESTfulResult<PagedList<BackgroundTaskOutput>> GetBackgroundTaskPage(BackgroundTaskPageInput input)
    {
       
        var query = SpareTime.GetWorkers().AsQueryable();


        var list= query.Skip((input.PageIndex - 1) * input.PageIndex)
            .Take(input.PageSize).Select(x=>new BackgroundTaskOutput()
            {
                TaskName = x.WorkerName,
                Description = x.Description,
                 TaskState= x.Enabled==true?"正在运行":"暂停"
            }).ToPagedList();
        
        return new RESTfulResult<PagedList<BackgroundTaskOutput>>() { Succeeded = true, Data = list };

    }

    public RESTfulResult<bool> StopTaskByName(string taskName)
    {
        //查询是否存在任务
        try
        {
            SpareTime.GetWorker(taskName).Stop();
        
               return new RESTfulResult<bool> { Succeeded = true };
         
        }
        catch (Exception e)
        {
            return new RESTfulResult<bool> { Succeeded = false };
        }
        
    }

    public RESTfulResult<bool> RestartTaskByName(string taskName)
    {
        //查询是否存在任务
        try
        {
            SpareTime.GetWorker(taskName).Start();

            return new RESTfulResult<bool> { Succeeded = true };

        }
        catch (Exception e)
        {
            return new RESTfulResult<bool> { Succeeded = false };
        };
    }

    public async  Task< RESTfulResult<bool>> CreateNewAutomation(AutomationInput input)
    {


       
        var entity= input.Adapt<Automation>();
       
         input.Actions.ForEach( async x =>
        {
           var find=  entity.Actions.FirstOrDefault(y => y.TargetDeviceId == x.TargetDeviceId);
           if (find != null)
           {
               
               // 获得get数据
               var getparamsres =await this.deviceService.GetGetDeviceCommandParamsbyDeviceId(find.TargetDeviceId);
              if (getparamsres.Succeeded)
              {
                  find.Parameters = new List<AutomationActionInputParam>();
                  var gets= getparamsres.Data;

                  foreach (KeyValuePair<string, string> keyValuePair in gets)
                  {
                      find.Parameters.Add(new AutomationActionInputParam()
                          { Name = keyValuePair.Key, Value = keyValuePair.Value, Type = ActionValueType.Get });
                  }

                 
                  x.SetParameters.ForEach(p =>
                  {
                      find.Parameters.Add(new AutomationActionInputParam() { Name = p.Name, Value = p.Value, Type = ActionValueType.Set });
                  });
              }
             
              
           }
        });

         await  this._automationRepository.InsertNowAsync(entity);




        return new RESTfulResult<bool>();




    }

    public void RigAutomatioinTask()
    {
        //获得所有的自动化

        var entities= this._automationRepository.AsQueryable().Include(x=>x.Actions).ThenInclude(x=>x.Parameters).Include(x=>x.Triggers).ToList();
        if (entities.Count == 0)
            return;
        this.pluginStateChangeNotification.AutomationTemps = new List<AutomationTemp>();
        foreach (var entity in entities)
        {
            var temp = new AutomationTemp();
            temp.TriggerType = entity.TriggerType.ToString();
            foreach (var action in entity.Actions)
            {
                var device= this.deviceServiRepository.AsQueryable().Include(x=>x.Type).FirstOrDefault(x => x.Id == action.TargetDeviceId);
                if (device == null)
                    Oops.Oh($"自动化初始化没有发现设备{action.TargetDeviceId}");

                var actionTemp = new AutomationActionTemp();
                
                actionTemp.TargetDeviceName = device.Name;
                // 获得插件
                actionTemp.pluginPath = device.Type.PluginPath;
                actionTemp.PluginTypeKey = device.Type.Key;

                foreach (var parameter in action.Parameters)
                {
                    if (parameter.Type == ActionValueType.Get)
                    {
                        actionTemp.GetParameters.Add(new DeviceInputParameter(parameter.Name,parameter.Value));
                    }
                    else
                    {
                        actionTemp.SetParameters.Add(new DeviceInputParameter(parameter.Name,parameter.Value));
                    }
                }
              
                
                
                temp.Actions.Add(actionTemp);
            }

            foreach (var trigger in entity.Triggers)
            {
                var triggerTemp = new AutomationTriggerTemp();

                triggerTemp.Property = trigger.Property;
                triggerTemp.BeforeValue = trigger.BeforeValue;
                triggerTemp.AfterValue = trigger.AfterValue;
                
                
                var device= this.deviceServiRepository.AsQueryable().Include(x=>x.Type).FirstOrDefault(x => x.Id == trigger.DeviceId);
                if (device == null)
                    Oops.Oh($"自动化初始化没有发现设备{trigger.DeviceId}");

                triggerTemp.DeviceName = device.Name;

                temp.Triggers.Add(triggerTemp);
            }
            
            this.pluginStateChangeNotification.AutomationTemps.Add(temp);
        }
        

    }
}
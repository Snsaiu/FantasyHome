using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.DevicePluginInterface;

using Furion.DatabaseAccessor;
using Furion.TaskScheduler;

using Mapster;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter;

public class BackgroundTaskService:IBackgroundTaskService,IDynamicApiController,ITransient
{
    private readonly IRepository<Automation> _automationRepository;
    private readonly PluginStateChangeNotification pluginStateChangeNotification;

    public BackgroundTaskService(IRepository<Automation> automationRepository,PluginStateChangeNotification pluginStateChangeNotification)
    {
        _automationRepository = automationRepository;
        this.pluginStateChangeNotification = pluginStateChangeNotification;
    }

    public RESTfulResult<PagedList<BackgroundTaskOutput>> GetBackgroundTaskPage(BackgroundTaskPageInput input)
    {
       
        var query = SpareTime.GetWorkers().AsQueryable();


        var list= query.Skip((input.PageIndex - 1) * input.PageIndex)
            .Take(input.PageSize).Select(x=>new BackgroundTaskOutput()
            {
                TaskName = x.WorkerName,
                Description = x.Description,
                 TaskState= x.Enabled==true?"��������":"��ͣ"
            }).ToPagedList();
        
        return new RESTfulResult<PagedList<BackgroundTaskOutput>>() { Succeeded = true, Data = list };

    }

    public RESTfulResult<bool> StopTaskByName(string taskName)
    {
        //��ѯ�Ƿ��������
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
        //��ѯ�Ƿ��������
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

    public RESTfulResult<bool> CreateNewAutomation(AutomationInput input)
    {


        var entity= input.Adapt<Automation>();

        this._automationRepository.InsertNow(entity);

        //todo ����Զ���
        return null;

        
        

    }

    public void RigAutomatioinTask()
    {
        //������е��Զ���

        var entities= this._automationRepository.AsQueryable().ToList();

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;
using Furion.DatabaseAccessor;
using Furion.TaskScheduler;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter;

public class BackgroundTaskService:IBackgroundTaskService,IDynamicApiController,ITransient
{
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
}
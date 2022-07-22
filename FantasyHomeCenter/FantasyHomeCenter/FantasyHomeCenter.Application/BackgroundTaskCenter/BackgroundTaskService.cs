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
                TaskState = x.Status.ToString()
            }).ToPagedList();
        
        return new RESTfulResult<PagedList<BackgroundTaskOutput>>() { Succeeded = true, Data = list };

    }
}
using System.Collections.Generic;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter;

public interface IBackgroundTaskService
{
    RESTfulResult<PagedList<BackgroundTaskOutput>> GetBackgroundTaskPage(BackgroundTaskPageInput input);

    /// <summary>
    /// 暂停一个任务
    /// </summary>
    /// <param name="taskName">任务名称</param>
    /// <returns></returns>
    RESTfulResult<bool> StopTaskByName(string taskName);
    

    /// <summary>
    /// 重新启动任务
    /// </summary>
    /// <param name="taskName"></param>
    /// <returns></returns>
    RESTfulResult<bool> RestartTaskByName(string taskName);
    
}
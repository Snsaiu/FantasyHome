using System.Collections.Generic;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter;

public interface IBackgroundTaskService
{
    RESTfulResult<PagedList<BackgroundTaskOutput>> GetBackgroundTaskPage(BackgroundTaskPageInput input);

    /// <summary>
    /// ��ͣһ������
    /// </summary>
    /// <param name="taskName">��������</param>
    /// <returns></returns>
    RESTfulResult<bool> StopTaskByName(string taskName);
    

    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="taskName"></param>
    /// <returns></returns>
    RESTfulResult<bool> RestartTaskByName(string taskName);
    
}
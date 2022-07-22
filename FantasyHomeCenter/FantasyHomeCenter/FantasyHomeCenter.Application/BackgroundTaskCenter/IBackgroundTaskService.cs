using System.Collections.Generic;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter;

public interface IBackgroundTaskService
{
    RESTfulResult<PagedList<BackgroundTaskOutput>> GetBackgroundTaskPage(BackgroundTaskPageInput input);

    
}
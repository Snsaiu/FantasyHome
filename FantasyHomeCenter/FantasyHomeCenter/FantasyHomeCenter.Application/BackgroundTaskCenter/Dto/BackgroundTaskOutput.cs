using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

public class BackgroundTaskOutput
{
    [Display(Name = "任务名称")]
    public string TaskName { get; set; }

    [Display(Name = "任务描述")]
    public string Description { get; set; }

    [Display(Name = "任务状态")]
    public string TaskState { get; set; }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Core.Enums;

[Description("设备状态")]
public enum DeviceState
{
    [Display(Name = "正在运行")]
    [Description("正在运行")]
    Running,
    [Display(Name = "关闭")]
    Closed,
    [Display(Name = "已丢失")]
    Lost,
    [Display(Name = "正在睡眠")]
    Sleeping
}
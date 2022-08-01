using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FantasyHomeCenter.DevicePluginInterface;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class SetDeviceStateInput
{
    public SetDeviceStateInput()
    {
        this.Values = new List<DeviceConstCommandParamsOutput>();
        this.CurrentStates = new List<KeyValue<string, string>>();
    }
    [Display(Name = "设备名称")]
    public string DeviceName { get; set; }

    [Display(Name = "房间")]
    public string Room { get; set; }

    [Display(Name = "控制器")]
    public DeviceControllerBase Controller { get; set; }
    
    [Display(Name = "插件路径")]
    public string  PluginPath { get; set; }

    [Required(ErrorMessage = "该项不能为空")]
    public List<DeviceConstCommandParamsOutput> Values { get; set; }

    [Display(Name = "必填参数")]
    public Dictionary<string,string> SetCommandConstParams { get; set; }

    
    public List<KeyValue<string,string>> CurrentStates { get; set; }
}
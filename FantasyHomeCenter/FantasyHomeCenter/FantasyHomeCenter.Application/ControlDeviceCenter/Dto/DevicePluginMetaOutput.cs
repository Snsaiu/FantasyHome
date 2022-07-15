using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.ControlDeviceCenter.Dto;

public class DevicePluginMetaOutput
{
    
    public string Key { get; set; }
    
    public List<DeviceMetaOutput> Devices { get; set; }
}

public class DeviceMetaOutput
{
    public string Name { get; set; }

    public string Description { get; set; }

    public RoomMetaOutput Room { get; set; }

    public List<DeviceCommandParamOutput> ConstCommandParams { get; set; }
}

public class RoomMetaOutput
{
    public string RoomName { get; set; }
}

public class DeviceCommandParamOutput
{
    public string Name { get; set; }

    public string Value { get; set; }

    public CommandParameterTypeOutput Type { get; set; }
}

public enum CommandParameterTypeOutput
{
    [Display(Name ="初始化")]
    Init,
    [Display(Name = "设置")]
    Set,
    [Display(Name = "获得")]
    Get
}
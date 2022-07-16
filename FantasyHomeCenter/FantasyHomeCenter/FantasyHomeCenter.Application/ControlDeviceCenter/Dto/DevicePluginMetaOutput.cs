using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FantasyHomeCenter.DevicePluginInterface;

namespace FantasyHomeCenter.Application.ControlDeviceCenter.Dto;

public class DevicePluginMetaOutput
{
    
    public string Key { get; set; }
    
    public List<DeviceMetaOutput> Devices { get; set; }
}

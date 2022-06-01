using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class DeviceOutput
{
    [Display(Name = "设备类型")]
    public string Type { get; set; }

    [Display(Name = "设备名称")]
    public string Name { get; set; }
    
    public int Id { get; set; }
    
}
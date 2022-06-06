using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class DeviceTypeOutput
{

    [Display(Name = "设备类型")]
    public string DeviceTypeName { get; set; }

    public int  Id { get; set; }

    [Display(Name ="Key")]
    public string Key { get; set; }

    [Display(Name ="版本")]
    public string Version { get; set; }

    [Display(Name = "作者")]

    public string Author { get; set; }

    [Display(Name ="描述")]
    public string PluginDescription { get; set; }
    
}
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.ControlDeviceCenter.Dto;

public class ControlDeviceOutput
{
    [Display(Name = "设备名称")]
    public string Name { get; set; }

    [Display(Name = "设备唯一编码")]
    public string DeviceCode { get; set; }
    
    [Display(Name = "设备ip地址")]
    public string Ip { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    [Display(Name = "设备类型")]
    public string DeviceType { get; set; }

    [Display(Name = "所属房间")]
    public string Room { get; set; }
}
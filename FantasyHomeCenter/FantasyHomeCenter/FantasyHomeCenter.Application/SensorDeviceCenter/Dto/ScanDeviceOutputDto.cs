using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.SensorDeviceCenter.Dto;

/// <summary>
/// 未被添加到数据库的设备
/// </summary>
public class ScanDeviceOutputDto
{
    /// <summary>
    /// guid
    /// </summary>
    [Display(Name = "唯一编号")]
    public string Guid { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    [Display(Name = "设备名称")]
    public string DeviceName { get; set; }

    /// <summary>
    /// 设备地址
    /// </summary>
    [Display(Name = "设备IP地址")]
    public string Ip { get; set; }
}
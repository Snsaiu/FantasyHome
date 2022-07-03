namespace FantasyHomeCenter.Application.SensorDeviceCenter.Dto;

/// <summary>
/// 未被添加到数据库的设备
/// </summary>
public class ScanDeviceOutputDto
{
    /// <summary>
    /// guid
    /// </summary>
    public string Guid { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    /// 设备地址
    /// </summary>
    public string Ip { get; set; }
}
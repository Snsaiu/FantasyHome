namespace FantasyHomeCenter.Application.ControlDeviceCenter.Dto;

public class RegistMachineInput
{
    /// <summary>
    /// 设备名称
    /// </summary>
    public string MachineName { get; set; }

    /// <summary>
    /// 设备地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 设备唯一编码
    /// </summary>
    public string MachineCode { get; set; }

    /// <summary>
    /// 验证用户名
    /// </summary>
    public string ValidateUserName { get; set; }

    /// <summary>
    /// 验证用户名密码
    /// </summary>
    public string ValidateUsePassword { get; set; }
    
    public string DeviceType { get; set; }
}
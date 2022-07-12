namespace FantasyHomeCenter.Core.Entities;

[Description("交互设备")]
public class UiDevice:Entity
{
    public UiDevice()
    {
        this.CreatedTime = DateTime.Now;
    }

    [Required]
    [Description("设备拥有者")]
    public Family Family { get; set; }
    
    [Description("设备所在房间")]
    public Room? Room { get; set; }
    
    [Required]
    [Description("设备名称")]
    public string Name { get; set; }

    [Required]
    [Description("设备唯一编码")]
    public string DeviceCode { get; set; }

    [Description("设备ip地址")]
    public string Ip { get; set; }

    [Required]
    [Description("交互设备类型")]
    public UiDeviceType UiDeviceType { get; set; }
}
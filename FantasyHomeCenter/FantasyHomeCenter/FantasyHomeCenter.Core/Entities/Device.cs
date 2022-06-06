using FantasyHomeCenter.Core.Enums;

namespace FantasyHomeCenter.Core.Entities;

[Description("设备类型")]
public class Device:Entity
{

    public Device()
    {
        this.CreatedTime=DateTimeOffset.Now;
    }

    [Description("设备描述")]
    public string Description { get; set; }
    
    [Description("设备类型")]
    public  DeviceType Type { get; set; }

    [Description("设备名称")]
    public string Name { get; set; }
    
    [Description( "id")]
    public int Id { get; set; }

    [Description("IP地址")]
    public string Address { get; set; }
    
    [Description("设备状态")]
    public DeviceState State { get; set; }

    [Description("配置参数")]
    public string Config { get; set; }
    
    [Description("设备所在房间")]
    public Room Room { get; set; }

    public virtual ICollection<CommandConstParams> ConstCommandParams { get; set; }
}
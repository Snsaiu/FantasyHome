

namespace FantasyHomeCenter.Core.Entities;

[Description("设备类型")]
public class DeviceType:Entity
{

    public DeviceType()
    {
        this.CreatedTime = DateTime.Now;
        this.Devices = new List<Device>();
    }
    
    [Required]
    [Description("设备类型")]
    public string DeviceTypeName { get; set; }

    public virtual ICollection<Device> Devices { get; set; }
    
}
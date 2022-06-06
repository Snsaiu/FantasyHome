

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

    [Description("插件唯一编号")]
    public string Key { get; set; }


    [Description("插件路径")]
    public string PluginPath { get; set; }


    [Description("插件文件名称")]
    public string PluginName { get; set; }

    [Description("插件作者")]
    public string Author { get; set; }

    [Description("插件版本")]
    public string Version { get; set; }

    [Description("插件描述")]
    public string  PluginDescription { get; set; }

    public virtual ICollection<Device> Devices { get; set; }


    
}
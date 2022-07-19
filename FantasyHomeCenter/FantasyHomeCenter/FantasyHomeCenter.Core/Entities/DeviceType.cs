

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    [Column(TypeName ="text")]
    public string DeviceTypeName { get; set; }

    [Description("插件唯一编号")]
    [Column(TypeName = "text")]
    public string Key { get; set; }


    [Description("插件路径")]
    [Column(TypeName = "text")]
    public string PluginPath { get; set; }


    [Description("插件文件名称")]
    [Column(TypeName = "text")]
    public string PluginName { get; set; }

    [Description("插件作者")]
    [Column(TypeName = "text")]
    public string Author { get; set; }

    [Description("插件版本")]
    [Column(TypeName = "text")]
    public string Version { get; set; }

    [Description("插件描述")]
    [Column(TypeName = "text")]
    public string  PluginDescription { get; set; }

    public virtual ICollection<Device> Devices { get; set; }



}
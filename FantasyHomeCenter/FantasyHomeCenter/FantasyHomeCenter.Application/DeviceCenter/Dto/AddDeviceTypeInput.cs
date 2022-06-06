using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class AddDeviceTypeInput
{
    [Required(ErrorMessage = "设备类型不能为空")]
    public string DeviceTypeName { get; set; }

    [Required]
    [Description("插件作者")]
    public string Author { get; set; }

    [Required(ErrorMessage ="key不能为空")]
    public string Key { get; set; }

    [Required]
    [Description("插件文件名")]
    public string PluginName { get; set; }
    /// <summary>
    /// 插件路径
    /// </summary>
    [Required(ErrorMessage ="插件路径")]
    public string PluginPath { get; set; }
    [Description("插件描述")]
    public string PluginDescription { get; set; }
    [Required]
    [Description("插件版本")]
    public string Version { get; set; }
}
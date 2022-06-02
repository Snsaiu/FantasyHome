using System.ComponentModel.DataAnnotations;
using FantasyHomeCenter.Core.Enums;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class DeviceOutput
{
    [Display(Name = "设备类型")]
    public string Type { get; set; }

    [Display(Name = "设备名称")]
    public string Name { get; set; }
    
    [Display(Name = "id")]
    public int Id { get; set; }

    [Display(Name = "IP地址")]
    public string Address { get; set; }

    [Display(Name= "设备类型id")]
    public int DeviceTypeId { get; set; }
    
    [Display(Name = "设备描述信息")]
    public string Description { get; set; }
    
    [Display(Name = "当前状态")]
    public DeviceState State { get; set; }
}
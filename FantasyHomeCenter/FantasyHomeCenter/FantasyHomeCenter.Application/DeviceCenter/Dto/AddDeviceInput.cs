using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class AddDeviceInput
{

    [Display(Name = "设备描述")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "设备地址不能为空")]
    [Display(Name = "设备地址")]
    public string DeviceAddress { get; set; }
    
    [Required(ErrorMessage = "设备名称为空")]
    [Display(Name = "设备名称")]
    public string  DeviceName { get; set; }
    
    [Display(Name = "设备类型id")]
    public int DeviceId { get; set; }
 
}
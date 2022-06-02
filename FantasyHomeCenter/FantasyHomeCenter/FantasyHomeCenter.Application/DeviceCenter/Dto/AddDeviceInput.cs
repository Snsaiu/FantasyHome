using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class AddDeviceInput
{

    [Display(Name = "设备描述")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "设备地址不能为空")]
    [Display(Name = "设备地址")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "设备名称为空")]
    [Display(Name = "设备名称")]
    public string  Name { get; set; }
    
    [Display(Name = "设备类型id")]
    public int DeviceTypeId { get; set; }
    
    public int RoomId { get; set; }
 
}
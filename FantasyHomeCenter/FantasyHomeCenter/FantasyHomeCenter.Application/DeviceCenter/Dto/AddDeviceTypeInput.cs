using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class AddDeviceTypeInput
{
    [Required(ErrorMessage = "设备类型不能为空")]
    public string TypeName { get; set; }
}
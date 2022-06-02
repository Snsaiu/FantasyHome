using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class DeviceTypeOutput
{

    [Display(Name = "设备类型")]
    public string DeviceTypeName { get; set; }

    public int  Id { get; set; }
}
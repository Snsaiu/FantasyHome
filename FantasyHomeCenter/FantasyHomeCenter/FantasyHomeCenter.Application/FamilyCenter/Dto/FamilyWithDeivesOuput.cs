using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.FamilyCenter.Dto;

public class FamilyWithDeivesOuput
{
    public FamilyWithDeivesOuput()
    {
        this.Devices = new();
    }
    
    
    public int Id { get; set; }
    
    [Display(Name = "家人姓名")]
    public string UserName { get; set; }

    [Display(Name = "家人联系号码")]
    public string PhoneNumber { get; set; }

    public List<ControlDeviceOutput> Devices { get; set; }
}


public class ControlDeviceOutput
{

    public int Id { get; set; }
    
    [Display(Name = "设备名称")]
    public string Name { get; set; }

    [Display(Name = "设备唯一编码")]
    public string DeviceCode { get; set; }
    
    [Display(Name = "设备ip地址")]
    public string Ip { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    [Display(Name = "设备类型")]
    public string DeviceType { get; set; }

    [Display(Name = "所属房间")]
    public string Room { get; set; }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class AddDeviceInput
{
    public AddDeviceInput()
    {
        Parameters = new List<DeviceConstCommandParamsOutput>();
        this.InitCommandParams = new();
        this.SetCommandParams = new ();
        this.GetCommandParams = new ();
    }

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

    [Display(Name ="命令常量参数")]
    public List<DeviceConstCommandParamsOutput> Parameters { get; set; }

    public List<KeyValue<string,string>> InitCommandParams { get; set; }
    public List<KeyValue<string,string>> SetCommandParams { get; set; }
    public List<KeyValue<string,string>> GetCommandParams { get; set; }




}
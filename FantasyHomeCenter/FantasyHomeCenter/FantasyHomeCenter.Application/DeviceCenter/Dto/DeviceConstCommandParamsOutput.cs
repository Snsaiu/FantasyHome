using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.Core.Enums;

namespace FantasyHomeCenter.Application.DeviceCenter.Dto;

public class DeviceConstCommandParamsOutput
{
    public DeviceConstCommandParamsOutput()
    {
        this.List = new List<KeyValue<string, string>>();
    }
    [Required]
    [Display(Name ="键")]
    public string Name { get; set; }

    [Display(Name ="值")]
    public string Value { get; set; }

    [Required]
    [Display(Name ="类型")]
    public CommandParameterType Type { get; set; }

    [Description("该键值对是否是列表")]
    public bool IsList { get; set; }

    [Description("若键值对是列表，该属性存放列表中的具体元素")]
    public List<KeyValue<string,string>> List { get; set; }
}

public class KeyValue<K,T>
{
    [Required(ErrorMessage = "必填")]
    public K Key { get; set; }
    [Required(ErrorMessage = "必填")]
    public T Value { get; set; }
}
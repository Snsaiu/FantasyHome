using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using FantasyHomeCenter.Application.DeviceCenter.Dto;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

public class ActionParams
{

    public ActionParams()
    {
        this.Items = new List<KeyValue<string, string>>();
    }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Value { get; set; }

    [Required]
    public bool ReadOnly { get; set; } = false;

    public bool IsEnum { get; set; } = false;

    public List<KeyValue<string, string>> Items { get; set; }


}
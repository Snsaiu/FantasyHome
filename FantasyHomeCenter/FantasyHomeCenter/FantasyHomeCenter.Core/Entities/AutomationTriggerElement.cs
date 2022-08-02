namespace FantasyHomeCenter.Core.Entities;

public class AutomationTriggerElement:Entity
{


    public Automation Automation { get; set; }


    [Required(ErrorMessage = "原设备不能为空")]
    [Display(Name = "设备Id")]
    public int DeviceId { get; set; }


    [Required(ErrorMessage = "属性不能为空")]
    [Display(Name = "属性")]
    public string Property { get; set; }


    [Required(ErrorMessage = "原始值不能为空")]
    [Display(Name = "改变之前的属性值")]
    public string BeforeValue { get; set; }

    [Required(ErrorMessage = "原始值不能为空")]
    [Display(Name = "改变之后的属性值")]
    public string AfterValue { get; set; }
}
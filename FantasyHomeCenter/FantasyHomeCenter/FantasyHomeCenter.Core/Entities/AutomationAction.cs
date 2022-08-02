namespace FantasyHomeCenter.Core.Entities;

public class AutomationAction:Entity
{

    public Automation Automation { get; set; }

    [Display(Name = "属性")]
    public string Property { get; set; }


    [Display(Name = "值")]
    public string Value { get; set; }

    [Display(Name = "目标设备")]
    public int TargetDeviceId { get; set; }
}
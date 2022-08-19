

using System.Collections.Generic;

namespace FantasyHomeCenter.Core.Entities;

public class AutomationAction:Entity
{


    public AutomationAction()
    {
        this.Parameters = new List<AutomationActionInputParam>();
      
    }


    public Automation Automation { get; set; }


    public virtual ICollection<AutomationActionInputParam> Parameters { get; set; }


    [Display(Name = "目标设备")]
    public int TargetDeviceId { get; set; }

   
}
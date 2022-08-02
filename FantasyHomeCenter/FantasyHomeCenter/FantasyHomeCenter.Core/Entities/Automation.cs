using System.Collections.Generic;

using FantasyHomeCenter.Core.Enums;

namespace FantasyHomeCenter.Core.Entities;

public class Automation : Entity
{


    public Automation()
    {
        this.Triggers = new List<AutomationTriggerElement>();
        this.Actions = new List<AutomationAction>();
    }



    [Required]
    [Description("自动化描述")]
    public string Summary { get; set; }

    [Required(ErrorMessage = "触发器类型不能为空")]

    [Description("触发器类型")]
    public TriggerType TriggerType { get; set; } = TriggerType.状态触发器;


    /// <summary>
    /// 触发器
    /// </summary>
    public virtual ICollection<AutomationTriggerElement> Triggers { get; set; }
    /// <summary>
    /// 动作
    /// </summary>
    public virtual ICollection<AutomationAction> Actions { get; set; }
}
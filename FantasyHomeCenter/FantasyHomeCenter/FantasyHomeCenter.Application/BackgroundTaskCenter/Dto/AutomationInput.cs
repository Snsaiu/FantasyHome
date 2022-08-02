using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Core.Enums;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter.Dto
{
    /// <summary>
    /// 自动化
    /// </summary>
    public class AutomationInput
    {

        [Display(Name = "触发器类型")]
        public TriggerType TriggerType { get; set; } = TriggerType.状态触发器;


        public int  DeviceId{ get; set; }

        public string Property { get; set; }

        /// <summary>
        /// 属性原始值
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 属性变换后的值
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 触发器
        /// </summary>
        public List<TriggerElementInput> Triggers { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        public List<ActionInput> Actions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter.Dto
{
    /// <summary>
    /// 自动化
    /// </summary>
    public class AutomationInput
    {

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

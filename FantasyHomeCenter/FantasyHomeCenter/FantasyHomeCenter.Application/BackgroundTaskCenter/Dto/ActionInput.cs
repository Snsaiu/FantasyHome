using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter.Dto
{
    /// <summary>
    /// 动作
    /// </summary>
    public class ActionInput
    {
        public ActionInput()
        {
            this.SetParameters = new List<ActionParams>();
        }

        public List<ActionParams> SetParameters { get; set; }

        [Display(Name ="目标设备")]
        public int TargetDeviceId { get; set; }
    }
}

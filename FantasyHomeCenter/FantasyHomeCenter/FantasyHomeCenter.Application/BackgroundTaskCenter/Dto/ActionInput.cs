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
        [Display(Name ="属性")]
        public string Property { get; set; }


        [Display(Name ="值")]
        public string Value { get; set; }

        [Display(Name ="目标设备")]
        public int TargetDeviceId { get; set; }
    }
}

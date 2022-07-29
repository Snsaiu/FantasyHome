using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FantasyHomeCenter.Core.Enums;

namespace FantasyHomeCenter.Application.BackgroundTaskCenter.Dto
{
    public class TriggerElementInput
    {


        [Display(Name ="触发器类型")]
        public TriggerType TriggerType { get; set; }

        [Display(Name ="设备")]
        public int DeviceId { get; set; }

        [Display(Name ="条件类型")]
        public ConditionType ConditionType { get; set; }

        [Display(Name ="属性")]
        public string Property { get; set; }


        [Display(Name ="属性符号")]
        public StateTag StateTag { get; set; }

        [Display(Name ="改变之前的属性值")]
        public string BeforeValue { get; set; }

        [Display(Name ="改变之后的属性值")]
        public string AfterValue { get; set; }  

        [Display(Name ="属性值类型")]
        public ValueTag ValueTag { get; set; }
    }

   


   
}

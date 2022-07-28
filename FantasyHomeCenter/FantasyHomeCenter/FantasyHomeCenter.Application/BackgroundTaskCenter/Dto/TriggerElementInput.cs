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

        [Display(Name ="条件类型")]
        public ConditionType ConditionType { get; set; }

        [Display(Name ="属性")]
        public string Property { get; set; }


        [Display(Name ="属性符号")]
        public StateTag StateTag { get; set; }

        [Display(Name ="属性值")]
        public string Value { get; set; }


        [Display(Name ="属性值类型")]
        public ValueTag ValueTag { get; set; }
    }

   


   
}

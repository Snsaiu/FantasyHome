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



        [Required(ErrorMessage ="原设备不能为空")]
        [Display(Name ="设备Id")]
        public int DeviceId { get; set; }


        [Required(ErrorMessage ="属性不能为空")]
        [Display(Name ="属性")]
        public string Property { get; set; }


        [Required(ErrorMessage ="原始值不能为空")]
        [Display(Name ="改变之前的属性值")]
        public string BeforeValue { get; set; }

        [Required(ErrorMessage = "原始值不能为空")]
        [Display(Name ="改变之后的属性值")]
        public string AfterValue { get; set; }  

    }

   


   
}

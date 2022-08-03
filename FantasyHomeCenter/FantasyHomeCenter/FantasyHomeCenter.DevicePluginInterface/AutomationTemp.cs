using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FantasyHomeCenter.DevicePluginInterface
{
    public class AutomationTemp
    {

        public string TriggerType { get; set; }

        public List<AutomationTriggerTemp> Triggers { get; set; }

        public List<AutomationActionTemp> Actions { get; set; }

        public AutomationTemp()
        {
            this.Triggers = new List<AutomationTriggerTemp>();
            this.Actions = new List<AutomationActionTemp>();
        }


    }

    public class AutomationTriggerTemp
    {

       
        public string DeviceName { get; set; }


      
        public string Property { get; set; }


   
        public string BeforeValue { get; set; }

      
        public string AfterValue { get; set; }
    }

    public class AutomationActionTemp
    {


        public Dictionary<string,string> Data { get; set; }

     
        public string PluginTypeKey { get; set; }
    }
}

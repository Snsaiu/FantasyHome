using System.Collections.Generic;
using System.Windows.Controls;

namespace FantasyHomeCenter.DevicePluginInterface
{
  
    public abstract class ControlUI:UserControl
    {
        protected readonly DeviceMetaOutput deviceMetaOutput;

        public delegate void MessageSendDelegte(MessageModel model);
        public event MessageSendDelegte MqttMessageSendEvent;

        public string DeviceTypeKey{ get; set; }

        public string DeviceKey { get; set; }
        public abstract Dictionary<string,string> BuildInitRequest();
        
        public ControlUI(DeviceMetaOutput deviceMetaOutput)
        {
            this.deviceMetaOutput = deviceMetaOutput;
        }
        
        public void SendMessage(MessageModel model)
        {
            if (this.MqttMessageSendEvent!=null)
            {
                this.MqttMessageSendEvent(model);
            }
        }

        public abstract void UpdateState(Dictionary<string, string> data);

    }
}
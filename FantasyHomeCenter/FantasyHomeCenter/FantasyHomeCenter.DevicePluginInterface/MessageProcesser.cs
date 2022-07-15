using System.Threading.Tasks;
using MQTTnet.Client;

namespace FantasyHomeCenter.DevicePluginInterface
{
    public delegate void MqttMessageSendDelegte(string content);

    public class MessageProcesser
    {
        public event MqttMessageSendDelegte MqttMessageSendEvent;
        
       
        public void SendMessage(string content)
        {
            if (this.MqttMessageSendEvent!=null)
            {
                this.MqttMessageSendEvent(content);
            }
        }
        
    }
}
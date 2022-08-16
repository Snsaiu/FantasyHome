using System.Collections.Generic;
 

namespace FantasyHomeCenter.DevicePluginInterface
{
  
    public abstract class ControlUI:ContentPage
    {
        protected readonly DeviceMetaOutput deviceMetaOutput;

        public delegate void MessageSendDelegte(MessageModel model);
        public event MessageSendDelegte MqttMessageSendEvent;

        public string Topic { get; set; }

        /// <summary>
        /// ������͵�guid 
        /// </summary>
        public string DeviceTypeKey{ get; set; }

        /// <summary>
        /// �������� ���� ����
        /// </summary>
        public string RoomName { get; set; }

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
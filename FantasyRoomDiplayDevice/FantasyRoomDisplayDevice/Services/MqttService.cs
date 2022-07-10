using System.Text;
using FantasyRoomDisplayDevice.Models;
using MQTTnet;
using MQTTnet.Client;

namespace FantasyRoomDisplayDevice.Services
{
    public class MqttService
    {

        public delegate void ConnectedSuccessDelegate();

        public event ConnectedSuccessDelegate ConnectedSuccessEvent;

        public delegate void DisConnectedDelegate();

        public event DisConnectedDelegate DisConnectEvent;

        public delegate void MessageReceivedDelegate(MqttMessage data);

        public event MessageReceivedDelegate MessageReceivedEvent;
            
        
        private IMqttClient client = null;
        public MqttService()
        {
            var factory = new MqttFactory();
            this.client= factory.CreateMqttClient();
            this.client.ConnectAsync(this.initOptions());
            this.client.ConnectedAsync += async (s) =>
            {
                if (this.ConnectedSuccessEvent != null)
                {
                    this.ConnectedSuccessEvent();
                }
            };

            this.client.DisconnectedAsync += async (s) =>
            {
                if (this.DisConnectEvent != null)
                {
                    this.DisConnectEvent();
                }
            };
            this.client.ApplicationMessageReceivedAsync += async (s) =>
            {
                string content = Encoding.UTF8.GetString(s.ApplicationMessage.Payload);

                if (this.MessageReceivedEvent!=null)
                {
                    this.MessageReceivedEvent(new MqttMessage());
                }
                
            };

        }

        private MqttClientOptions initOptions()
        {
            MqttClientOptions opt = new MqttClientOptions();
            return opt;
        }
    }
}
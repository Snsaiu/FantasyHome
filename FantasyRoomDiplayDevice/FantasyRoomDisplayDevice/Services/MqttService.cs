using System;
using System.Text;
using FantasyRoomDisplayDevice.Models;
using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;

namespace FantasyRoomDisplayDevice.Services
{
    public class MqttService
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// 是否已经链接
        /// </summary>
        private bool connected = false;

        public delegate void ConnectedSuccessDelegate();

        public event ConnectedSuccessDelegate ConnectedSuccessEvent;

        public delegate void DisConnectedDelegate();

        public event DisConnectedDelegate DisConnectEvent;

        public delegate void MessageReceivedDelegate(MqttMessage data);

        public event MessageReceivedDelegate MessageReceivedEvent;

        public delegate void ConnectErrorDelegate(string error);

        public event ConnectErrorDelegate ConnectErrorEvent;
            
        
        private IMqttClient client = null;
        public MqttService(IConfiguration configuration)
        {
            this.configuration = configuration;

             if (!this.configuration.GetSection("MqttServer").Exists())
             {
                 if (this.ConnectErrorEvent != null)
                 {
                     this.ConnectErrorEvent("未发现可用的配置信息");
                 }
                 return;
                 
             }
            
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
            opt.ChannelOptions = new MqttClientTcpOptions()
            {
                Server = this.configuration.GetSection("MqttServer:Host").Value,
                Port = Convert.ToInt32(this.configuration.GetSection("MqttServer:Port").Value)
            };
            //opt.Timeout = -1;
            opt.CleanSession = true;
            return opt;
        }
    }
}
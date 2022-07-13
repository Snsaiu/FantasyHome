using System;
using System.Text;
using System.Threading.Tasks;
using FantasyHome.Application;
using FantasyHome.Application.Dto;
using FantasyHome.Application.Impls;
using FantasyRoomDisplayDevice.Models;
using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;

namespace FantasyRoomDisplayDevice.Services
{
    public class MqttService
    {
        private readonly TempConfigService tempConfigService;

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
        public async Task<ResultBase<bool>> StartAsync()
        {
            if (string.IsNullOrEmpty(this.tempConfigService.MqttHost) ||
                string.IsNullOrEmpty(this.tempConfigService.MqttPort))
            {

                return new ResultBase<bool>() { Succeeded = false, Errors = "未发现配置信息" };
            }

            MqttConnectOption opt = new MqttConnectOption()
            {
                Host = this.tempConfigService.MqttHost,
                Port = this.tempConfigService.MqttPort
            };
           return await this.mqttApplication.ConnectAsync(opt);
        }

        private IMqttApplication mqttApplication;

        public ResultBase<bool> SendInfo(string content)
        {
           return this.mqttApplication.Send(content);
        }
        public MqttService(TempConfigService tempConfigService)
        {
            this.tempConfigService = tempConfigService;
            this.mqttApplication = new MqttApplication();
            
            this.mqttApplication.ConnectedSuccessEvent += async () =>
            {
                if (this.ConnectedSuccessEvent != null)
                {
                    this.ConnectedSuccessEvent();
                }
            };

            this.mqttApplication.DisConnectEvent += async () =>
            {
                if (this.DisConnectEvent != null)
                {
                    this.DisConnectEvent();
                }
            };
            this.mqttApplication.MessageReceivedEvent += async (s) =>
            {
               
                if (this.MessageReceivedEvent!=null)
                {
                    this.MessageReceivedEvent(s);
                }
                
            };

        }
        
    }
}
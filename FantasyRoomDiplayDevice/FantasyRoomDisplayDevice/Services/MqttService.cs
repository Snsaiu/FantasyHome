using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using FantasyHome.Application;
using FantasyHome.Application.Dto;
using FantasyHome.Application.Impls;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyRoomDisplayDevice.Models;
using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;

namespace FantasyRoomDisplayDevice.Services
{
    public class MqttService
    {
        private readonly TempConfigService tempConfigService;
        private readonly ILogger logger;

        /// <summary>
        /// 是否已经链接
        /// </summary>
        private bool connected = false;

        public delegate void ConnectedSuccessDelegate();

        public event ConnectedSuccessDelegate ConnectedSuccessEvent;

        public delegate void DisConnectedDelegate();

        public event DisConnectedDelegate DisConnectEvent;

        public delegate void MessageReceivedDelegate(MqttApplicationMessage data);

        public event MessageReceivedDelegate MessageReceivedEvent;

        public delegate void ConnectErrorDelegate(string error);

        public event ConnectErrorDelegate ConnectErrorEvent;

        public async Task SubscriptionAsync(List< MqttTopicFilter> filters)
        {
           await this.mqttApplication.SubscribeAsync(filters);
        }

        private string getGuid()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            String strHardDiskID = null;//存储磁盘序列号
            //调用ManagementObjectSearcher类的Get方法取得硬盘序列号
            foreach (ManagementObject mo in searcher.Get())
            {
                strHardDiskID = mo["SerialNumber"].ToString().Trim();//记录获得的磁盘序列号
                break;
            }
            return strHardDiskID;//显示硬盘序列号
        }

        public async Task<ResultBase<bool>> StartAsync()
        {
            if (string.IsNullOrEmpty(this.tempConfigService.MqttHost) ||
                string.IsNullOrEmpty(this.tempConfigService.MqttPort))
            {

                return new ResultBase<bool>() { Succeeded = false, Errors = "未发现配置信息" };
            }

            if (this.mqttApplication!=null)
            {
                return new ResultBase<bool>() { Succeeded = true };
            }
            MqttConnectOption opt = new MqttConnectOption()
            {
                Host = this.tempConfigService.MqttHost,
                Port = this.tempConfigService.MqttPort,
                ClientId=this.getGuid()
            };
            this.mqttApplication = new MqttApplication(opt);
            
            this.mqttApplication.ConnectedSuccessEvent += async () =>
            {
                this.logger.Info("mqtt连接成功回调成功");
                if (this.ConnectedSuccessEvent != null)
                {
                    this.ConnectedSuccessEvent();
                }
            };

            this.mqttApplication.DisConnectEvent += async () =>
            {
                this.logger.Info("mqtt断线，尝试重连...");
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
            this.mqttApplication.ReconnectEvent += () =>
            {

                this.logger.Info("mqtt自动重连成功!");
                return this.tempConfigService.MqttTopicFilters;
        
            };
          
           return await this.mqttApplication.ConnectAsync();
        }

        private IMqttApplication mqttApplication;

        public async Task<ResultBase<bool>> SendInfo(MqttApplicationMessage content)
        {
           return await this.mqttApplication.SendAsync(content);
        }
     
        public MqttService(TempConfigService tempConfigService,ILogger logger)
        {
            this.tempConfigService = tempConfigService;
            this.logger = logger;
        }
        
    }
}
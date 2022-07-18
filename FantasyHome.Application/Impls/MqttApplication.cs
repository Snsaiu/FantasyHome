using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FantasyHome.Application.Dto;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;

namespace FantasyHome.Application.Impls
{
    public class MqttApplication:IMqttApplication
    {
        private readonly MqttConnectOption connectOption;
        public event MessageReceivedDelegate? MessageReceivedEvent;
        public event ConnectedSuccessDelegate? ConnectedSuccessEvent;
        public event DisConnectedDelegate? DisConnectEvent;
        public event ReconnectDelegate? ReconnectEvent;


        private bool IsConnected = false;
        
        private IMqttClient client = null;

        public MqttApplication( MqttConnectOption connectOption)
        {
            this.connectOption = connectOption;
            var factory = new MqttFactory();
            this.client= factory.CreateMqttClient();
                  
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
                
                this._TryContinueConnect();
              
            };
            this.client.ApplicationMessageReceivedAsync += async (s) =>
            {

                var ss = s.ApplicationMessage;

                if (this.MessageReceivedEvent!=null)
                {
                    this.MessageReceivedEvent(s.ApplicationMessage);
                }
                
            };
        }
        
        private void _TryContinueConnect()
        {
            if (IsConnected) return;
 
            Thread retryThread = new Thread(new ThreadStart(delegate
            {
                while (this.client == null || !this.client.IsConnected)
                {
                   
 
                    if (this.client == null)
                    {
                        var factory = new MqttFactory();
                        this.client= factory.CreateMqttClient();
                        Thread.Sleep(3000);
                        continue;
                    }
                    
                    this.ConnectAsync();

                    // 如果还没连接不符合结束条件则睡2秒
                    if (!this.client.IsConnected)
                    {
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        if (this.ReconnectEvent != null)
                        {
                            var filiters = this.ReconnectEvent();
                            this.client.SubscribeAsync(new MqttClientSubscribeOptions()
                            {
                                TopicFilters = filiters
                            });
                        }
                    }
                }
            }));
             
            retryThread.Start();
        }
        
        private MqttClientOptions initOptions(MqttConnectOption options)
        {
            MqttClientOptions opt = new MqttClientOptions();
            opt.ChannelOptions = new MqttClientTcpOptions()
            {
                Server =options.Host,
                Port = Convert.ToInt32(options.Port),
               
                
            };
            //opt.Timeout = -1;
            opt.CleanSession = false;
           opt.ClientId = options.ClientId;
            return opt;
        }
        
        public async Task<ResultBase<bool>> ConnectAsync()
        {
         
       
            
    
            try
            {
                var res = await this.client.ConnectAsync(this.initOptions(this.connectOption));
            if (res.ResultCode==MqttClientConnectResultCode.Success)
            {
                return new ResultBase<bool>() { Succeeded = true };
            }
            else
            {
                return new ResultBase<bool>() { Succeeded = false,Errors = res.ResultCode};
            }
            }
            catch (Exception e)
            {
                return new ResultBase<bool>() { Succeeded = false,Errors = "mqtt 服务连接失败！"};
            }
        }

        public async Task<ResultBase<bool>> SendAsync(MqttApplicationMessage content)
        {
            var res= await this.client.PublishAsync(content);
            if (res.ReasonCode == MqttClientPublishReasonCode.Success)
            {
                return new ResultBase<bool>() { Succeeded = true };
            }
            return new ResultBase<bool>() { Succeeded = false };
        }

        public async Task SubscribeAsync(List< MqttTopicFilter> filters)
        {
          
            await  this.client.SubscribeAsync(new MqttClientSubscribeOptions()
          {
            TopicFilters = filters
          });
        }
    }
}
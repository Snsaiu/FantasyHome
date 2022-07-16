using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FantasyHome.Application.Dto;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;

namespace FantasyHome.Application.Impls
{
    public class MqttApplication:IMqttApplication
    {
        public event MessageReceivedDelegate? MessageReceivedEvent;
        public event ConnectedSuccessDelegate? ConnectedSuccessEvent;
        public event DisConnectedDelegate? DisConnectEvent;
    

        
        private IMqttClient client = null;
        
        private MqttClientOptions initOptions(MqttConnectOption options)
        {
            MqttClientOptions opt = new MqttClientOptions();
            opt.ChannelOptions = new MqttClientTcpOptions()
            {
                Server =options.Host,
                Port = Convert.ToInt32(options.Port),
               
                
            };
            //opt.Timeout = -1;
            opt.CleanSession = true;
           opt.ClientId = options.ClientId;
            return opt;
        }
        
        public async Task<ResultBase<bool>> ConnectAsync(MqttConnectOption connectOption)
        {
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
            };
            this.client.ApplicationMessageReceivedAsync += async (s) =>
            {

                var ss = s.ApplicationMessage;

                if (this.MessageReceivedEvent!=null)
                {
                    this.MessageReceivedEvent(s.ApplicationMessage);
                }
                
            };
            try
            {
                var res = await this.client.ConnectAsync(this.initOptions(connectOption));
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

        public async Task SubscribeAsync(MqttTopicFilter filter)
        {
          await  this.client.SubscribeAsync(filter);
        }
    }
}
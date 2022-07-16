using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHome.Application.Dto;
using MQTTnet;
using MQTTnet.Packets;

namespace FantasyHome.Application
{
    
    public delegate void MessageReceivedDelegate(MqttApplicationMessage data);

    public delegate void ConnectedSuccessDelegate();

   

    public delegate void DisConnectedDelegate();

    
    
    public interface IMqttApplication
    {
        public event MessageReceivedDelegate MessageReceivedEvent;
        public event ConnectedSuccessDelegate ConnectedSuccessEvent;
        public event DisConnectedDelegate DisConnectEvent;

        Task<ResultBase<bool>> ConnectAsync(MqttConnectOption connectOption);

        Task<ResultBase<bool>> SendAsync(MqttApplicationMessage content);

        Task SubscribeAsync(List< MqttTopicFilter> filters);
    }
}
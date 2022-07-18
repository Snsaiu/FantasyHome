using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHome.Application.Dto;
using MQTTnet;
using MQTTnet.Packets;

namespace FantasyHome.Application
{
    
    public delegate void MessageReceivedDelegate(MqttApplicationMessage data);

    public delegate void ConnectedSuccessDelegate();

    public delegate List<MqttTopicFilter> ReconnectDelegate();

    public delegate void DisConnectedDelegate();

    
    
    public interface IMqttApplication
    {
        public event MessageReceivedDelegate MessageReceivedEvent;
        public event ConnectedSuccessDelegate ConnectedSuccessEvent;
        public event DisConnectedDelegate DisConnectEvent;

        public event ReconnectDelegate ReconnectEvent;

      

        Task<ResultBase<bool>> ConnectAsync();

        Task<ResultBase<bool>> SendAsync(MqttApplicationMessage content);

        Task SubscribeAsync(List< MqttTopicFilter> filters);
    }
}
using System.Threading.Tasks;
using FantasyHome.Application.Dto;

namespace FantasyHome.Application
{
    
    public delegate void MessageReceivedDelegate(MqttMessage data);

    public delegate void ConnectedSuccessDelegate();

   

    public delegate void DisConnectedDelegate();

    
    
    public interface IMqttApplication
    {
        public event MessageReceivedDelegate MessageReceivedEvent;
        public event ConnectedSuccessDelegate ConnectedSuccessEvent;
        public event DisConnectedDelegate DisConnectEvent;

        Task<ResultBase<bool>> ConnectAsync(MqttConnectOption connectOption);

        ResultBase<bool> Send(string content);
    }
}
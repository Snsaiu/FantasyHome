using System.Threading.Tasks;

using FantasyHomeCenter.Application.MqttCenter.Dto;

namespace FantasyHomeCenter.Application.MqttCenter;

public interface IMqttServerInstance
{
    Task StartAsync();
    Task PublishAsync(string topic, MqttSendInfo data);
}
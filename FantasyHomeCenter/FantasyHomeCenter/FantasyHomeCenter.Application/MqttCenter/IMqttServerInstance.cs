using System.Threading.Tasks;

namespace FantasyHomeCenter.Application.MqttCenter;

public interface IMqttServerInstance
{
    Task StartAsync();
}
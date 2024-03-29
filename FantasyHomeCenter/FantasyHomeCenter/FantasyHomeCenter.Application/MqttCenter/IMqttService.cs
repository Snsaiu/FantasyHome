using FantasyHomeCenter.Application.MqttCenter.Dto.Service;
using FantasyHomeCenter.Application.MqttCenter.Dto.Client;
using FantasyHomeCenter.Application.MqttCenter.Dto.MqttSubscription;
using FantasyHomeCenter.Application.MqttCenter.Dto.Topic;

namespace FantasyHomeCenter.Application.MqttCenter;

public interface IMqttService
{
    /// <summary>
    /// 获得所有主题
    /// </summary>
    /// <returns></returns>
    MqttTopicListOutput GetTopicList();

    /// <summary>
    /// 获得所有客户端
    /// </summary>
    /// <returns></returns>
    MqttClientListOutput GetClientList();

    /// <summary>
    /// 获得订阅列表
    /// </summary>
    /// <returns></returns>
    MqttSubscriptionsOutput GetSubscriptions();
}
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.MqttCenter.Dto;

public class MqttTopicOutput
{
    public MqttTopicOutput(string topic, string clientId)
    {
        Topic = topic;
        ClientId = clientId;
    }

    public MqttTopicOutput()
    {
        
    }
    [Display(Name = "主题")]
    public string Topic { get; set; }
    [Display(Name = "客户端")]
    public string ClientId { get; set; }
}
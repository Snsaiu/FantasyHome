using Furion.ConfigurableOptions;

namespace FantasyHomeCenter.Application.MqttCenter.Dto;

public class MqttServiceOptions:IConfigurableOptions
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string UserName { get; set; }

    public string Password { get; set; }
}
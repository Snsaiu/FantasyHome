using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.MqttCenter.Dto;

public class MqttClientOuput
{

    public MqttClientOuput()
    {
        
    }


    public MqttClientOuput(string clientId, string endPoint)
    {
        ClientId = clientId;
        EndPoint = endPoint;
    }

    [Display(Name = "客户端")]
    public string ClientId { get; set; }

    [Display(Name = "地址")]
    public string EndPoint { get; set; }
}
using System;
using System.Text;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Client;
using FantasyHomeCenter.Application.MqttCenter.Dto.MqttSubscription;
using FantasyHomeCenter.Application.MqttCenter.Dto.Service;
using FantasyHomeCenter.Application.MqttCenter.Dto.Topic;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;


namespace FantasyHomeCenter.Application.MqttCenter;

public class MqttService:IMqttService,IDynamicApiController,ITransient
{
    private readonly IOptions<MqttServiceOptions> options;

    private string basicPwd = "";


    public MqttService(IOptions<MqttServiceOptions> options)
    {
        this.options = options;

        if (this.options.Value==null)
        {
            return;
            
        }
        string temp = $"{this.options.Value.UserName}:{this.options.Value.Password}";
        this.basicPwd="Basic "+ Convert.ToBase64String(new ASCIIEncoding().GetBytes(temp));
       
    }
    
    
    public MqttTopicListOutput GetTopicList()
    {
        if (this.options.Value==null)
        {
            return new MqttTopicListOutput() { code = -1 };

        }

        string http = $"http://{this.options.Value.Host}:{this.options.Value.Port}/api/v2/routes";
        var client = new RestClient(http);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Authorization", this.basicPwd);
        var body = @"";
        request.AddParameter("text/plain", body,  ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
       string content= response.Content;
       return JsonConvert.DeserializeObject<MqttTopicListOutput>(content);

    }

    public MqttClientListOutput GetClientList()
    {
        if (this.options.Value==null)
        {
            return new MqttClientListOutput() { code = -1 };

        }
        string http = $"http://{this.options.Value.Host}:{this.options.Value.Port}/api/v2/nodes/emq@127.0.0.1/clients";
        var client = new RestClient(http);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Authorization", "Basic YWRtaW46ZmxqMTIzNDU2");
        IRestResponse response = client.Execute(request);
        string content= response.Content;
        return JsonConvert.DeserializeObject<MqttClientListOutput>(content);
    }

    public MqttSubscriptionsOutput GetSubscriptions()
    {
        if (this.options.Value==null)
        {
            return new MqttSubscriptionsOutput() { code = -1 };
            
        }
        string http = $"http://{this.options.Value.Host}:{this.options.Value.Port}/api/v2/nodes/emq@127.0.0.1/subscriptions";
        var client = new RestClient(http);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Authorization", "Basic YWRtaW46ZmxqMTIzNDU2");
        IRestResponse response = client.Execute(request);
        string content= response.Content;
        return JsonConvert.DeserializeObject<MqttSubscriptionsOutput>(content);
        
    }
}
using System.Collections.Generic;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Client;
using FantasyHomeCenter.Application.MqttCenter.Dto.MqttSubscription;
using FantasyHomeCenter.Application.MqttCenter.Dto.Topic;
using Microsoft.Extensions.Caching.Distributed;
using MQTTnet.Server;
using Newtonsoft.Json;
using ObjectsItem = FantasyHomeCenter.Application.MqttCenter.Dto.Topic.ObjectsItem;

namespace FantasyHomeCenter.Application.MqttCenter;

public class LocalMqttService:IMqttService,IDynamicApiController,ITransient
{
    private readonly IDistributedCache distributedCache;


    public LocalMqttService(IDistributedCache distributedCache )
    {
        this.distributedCache = distributedCache;
    }
    public MqttTopicListOutput GetTopicList()
    {

       string content=  this.distributedCache.GetString("mqtt_topic");
       if (string.IsNullOrEmpty(content))
       {
           return new MqttTopicListOutput() { code = -1 };
       }

       var data = JsonConvert.DeserializeObject<List<MqttTopicOutput>>(content);
       MqttTopicListOutput output = new MqttTopicListOutput();

       output.code = 1;
       output.result.objects = new List<ObjectsItem>();
       foreach (var item in data)
       {
           output.result.objects.Add(new ObjectsItem() { topic = item.Topic, node = item.ClientId });
       }

       return output;
    }

    public MqttClientListOutput GetClientList()
    {
        string content=  this.distributedCache.GetString("mqtt_clients");
        if (string.IsNullOrEmpty(content))
        {
            return new MqttClientListOutput() { code = -1 };
        }

        var data = JsonConvert.DeserializeObject<List<MqttClientOuput>>(content);
        MqttClientListOutput output = new MqttClientListOutput();

        output.code = 1;
        output.result.objects = new List<Dto.Client.ObjectsItem>();
        foreach (var item in data)
        {
          
            output.result.objects.Add(new Dto.Client.ObjectsItem()
            {
                client_id = item.ClientId,
                ipaddress = item.EndPoint
            });
        }

        return output;
    }

    public MqttSubscriptionsOutput GetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}
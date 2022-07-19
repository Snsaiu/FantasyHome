using System.Collections.Generic;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Client;
using FantasyHomeCenter.Application.MqttCenter.Dto.MqttSubscription;
using FantasyHomeCenter.Application.MqttCenter.Dto.Topic;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using MQTTnet.Server;
using Newtonsoft.Json;
using ObjectsItem = FantasyHomeCenter.Application.MqttCenter.Dto.Topic.ObjectsItem;

namespace FantasyHomeCenter.Application.MqttCenter;

public class LocalMqttService:IMqttService,IDynamicApiController,ITransient
{
    private readonly IMemoryCache distributedCache;


    public LocalMqttService(IMemoryCache distributedCache )
    {
        this.distributedCache = distributedCache;
    }
    public MqttTopicListOutput GetTopicList()
    {

       var content=  this.distributedCache.Get<List<MqttTopicOutput>>("mqtt_topic");
       if (content==null||content.Count==0)
       {
           return new MqttTopicListOutput() { code = -1 };
       }

     
       MqttTopicListOutput output = new MqttTopicListOutput();

       output.code = 1;
       output.result.objects = new List<ObjectsItem>();
       foreach (var item in content)
       {
           output.result.objects.Add(new ObjectsItem() { topic = item.Topic, node = item.ClientId });
       }

       return output;
    }

    public MqttClientListOutput GetClientList()
    {
        var content=  this.distributedCache.Get<List<MqttClientOuput>>("mqtt_clients");
        if (content==null||content.Count==0)
        {
            return new MqttClientListOutput() { code = -1 };
        }

     
        MqttClientListOutput output = new MqttClientListOutput();

        output.code = 1;
        output.result.objects = new List<Dto.Client.ObjectsItem>();
        foreach (var item in content)
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Service;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;
using Newtonsoft.Json;
using StackExchange.Profiling.Internal;

namespace FantasyHomeCenter.Application.MqttCenter;

public class MqttServerInstance
{
    private readonly IDistributedCache distributedCache;

    private readonly string mqttClientKey = "mqtt_clients";

    private IMqttServer server;

    public async Task StartAsync()
    {
         server = new MqttFactory().CreateMqttServer();
        MqttServerOptionsBuilder serverOptions = new MqttServerOptionsBuilder();
        serverOptions.WithDefaultEndpointPort(1883);
      
        this.server.StartedHandler = new MqttServerStartedHandlerDelegate(s =>
        {
            Console.WriteLine("mqtt 服务已经启动！");
        });


        this.server.StoppedHandler = new MqttServerStoppedHandlerDelegate(s =>
        {
            Console.WriteLine("mqtt 服务已经停止");

        });
 
        //客户端连接
        this.server.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate(s =>
        {
            string clientid = s.ClientId;
            string endpoint = s.Endpoint;
            string content= this.distributedCache.GetString(this.mqttClientKey);
            if (string.IsNullOrEmpty(content))
            {
                List<MqttClientOuput> list = new();
                list.Add(new MqttClientOuput(clientid,endpoint));
                this.distributedCache.SetString(this.mqttClientKey,  list.ToJson());
            }
            else
            {
                var list=  JsonConvert.DeserializeObject<List<MqttClientOuput>>(content);
                if (!list.Any(x=>x.ClientId==clientid))
                {
                    list.Add(new MqttClientOuput(clientid,endpoint));
                   
                 
                     this.distributedCache.SetString(this.mqttClientKey,  list.ToJson());
                }
            }

        });  // += async s =>
        {
           
        };

        server.ClientDisconnectedHandler = new MqttServerClientDisconnectedHandlerDelegate(s =>
        {

            string clientid = s.ClientId;
            string endpoint = s.Endpoint;
            string content= this.distributedCache.GetString(this.mqttClientKey);
            if (!string.IsNullOrEmpty(content))
            {
                var list=  JsonConvert.DeserializeObject<List<MqttClientOuput>>(content);
                if (list.Any(x=>x.ClientId==clientid))
                {
                    list.Remove(list.First(y => y.ClientId == clientid));
                    string ser= JsonConvert.SerializeObject(list);
                     this.distributedCache.SetString(this.mqttClientKey, ser);
                }
            }

        });
        
        server.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(s =>
        {


        }); 
   
        await server.StartAsync(serverOptions.Build());

    }

    public MqttServerInstance(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    
        
    
    
    
}
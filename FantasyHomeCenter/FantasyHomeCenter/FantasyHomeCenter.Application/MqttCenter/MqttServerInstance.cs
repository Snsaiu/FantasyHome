using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Service;
using FantasyHomeCenter.Application.MqttCenter.Dto.Topic;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;
using Newtonsoft.Json;
using StackExchange.Profiling.Internal;

namespace FantasyHomeCenter.Application.MqttCenter;

public class MqttServerInstance:IMqttServerInstance,ISingleton
{
    private readonly IDistributedCache distributedCache;
    private readonly IDeviceService deviceService;

    private readonly string mqttClientKey = "mqtt_clients";

    private IMqttServer server;


    private readonly string mqttTopicKey = "mqtt_topic";
    
    public async Task StartAsync()
    {

     
        
        var devicetypes = this.deviceTypeRepository.AsEnumerable().ToList();
        
        foreach (var item in devicetypes)
        {
            if (string.IsNullOrEmpty(item.Key)==false)
            {
               await this.pluginService.AddPluginAsync(item.PluginPath, item.PluginName);
            }
        }
        
        
         server = new MqttFactory().CreateMqttServer();
        MqttServerOptionsBuilder serverOptions = new MqttServerOptionsBuilder();
        serverOptions.WithDefaultEndpointPort(1883);
      
        this.server.StartedHandler = new MqttServerStartedHandlerDelegate(s =>
        {
            Console.WriteLine("mqtt 服务已经启动！");
        });

        this.server.ClientUnsubscribedTopicHandler = new MqttServerClientUnsubscribedTopicHandlerDelegate((s) =>
        {
            string content= this.distributedCache.GetString(this.mqttTopicKey);
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            var list=  JsonConvert.DeserializeObject<List<MqttTopicOutput>>(content);
            if (list.Any(x=>x.ClientId==s.ClientId&&x.Topic==s.TopicFilter))
            {
                var x = list.First(x => x.ClientId == s.ClientId && x.Topic == s.TopicFilter);
                list.Remove(x);

                this.distributedCache.SetString(this.mqttTopicKey,  list.ToJson());
            }
        });

        this.server.ClientSubscribedTopicHandler = new MqttServerClientSubscribedTopicHandlerDelegate(s =>
        {
            string clientid = s.ClientId;
            string topic = s.TopicFilter.Topic;
            string content= this.distributedCache.GetString(this.mqttTopicKey);
            if (string.IsNullOrEmpty(content))
            {
                List<MqttTopicOutput> list = new();
                list.Add(new MqttTopicOutput(topic,clientid));
                this.distributedCache.SetString(this.mqttTopicKey,  list.ToJson());
            }
            else
            {
                var list=  JsonConvert.DeserializeObject<List<MqttTopicOutput>>(content);
                if (!list.Any(x=>x.ClientId==clientid&&x.Topic==topic))
                {
                    list.Add(new MqttTopicOutput(topic,clientid));
                   
                 
                    this.distributedCache.SetString(this.mqttTopicKey,  list.ToJson());
                }
            }

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

        }); 


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

            if (s.ApplicationMessage.Topic!="fantasyhome")
            {
                return;
            }

          string content=   Encoding.UTF8.GetString(s.ApplicationMessage.Payload);

          MqttSendInfo info = JsonConvert.DeserializeObject<MqttSendInfo>(content);
          if (string.IsNullOrEmpty(info.Topic) || string.IsNullOrEmpty(info.PluginKey) ||
              string.IsNullOrEmpty(info.DeviceName))
          {
                return;
          }
          RESTfulResult<Dictionary<string, string>> res = new RESTfulResult<Dictionary<string, string>>();
          if (info.CommandType==CommandType.Get)
          {
              res = this.deviceService.GetDeviceState(info);
          }
          else
          {
             res = this.deviceService.SetThenGetDeviceState(info);
          }
          
          MqttSendInfo sendinfo = new MqttSendInfo();
          if (res.Succeeded)
          {
              sendinfo.Success = true;
              sendinfo.Data = res.Data;
              sendinfo.DeviceName = info.DeviceName;
              sendinfo.PluginKey = info.PluginKey;
              sendinfo.Topic = info.Topic;
          }
          else
          {
              sendinfo.Success = false;
              sendinfo.ErrorMsg = res.Errors.ToString();
          }

          string sendcontent= JsonConvert.SerializeObject(sendinfo);
          //发送
          this.server.PublishAsync(new MqttApplicationMessage()
          {
              Topic = info.Topic,
              Payload = Encoding.UTF8.GetBytes(sendcontent)
          });
          
        }); 
   
        await server.StartAsync(serverOptions.Build());

    }

    private IRepository<DeviceType> deviceTypeRepository;
    private IPluginService pluginService;
    public MqttServerInstance(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;

        this.deviceTypeRepository = App.GetService<IRepository<DeviceType>>();
        this.deviceService = App.GetRequiredService<IDeviceService>();
        this.pluginService = App.GetRequiredService<IPluginService>();;
    }

    
        
    
    
    
}
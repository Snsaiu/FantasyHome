using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Service;
using FantasyHomeCenter.Application.MqttCenter.Dto.Topic;
using FantasyHomeCenter.Application.MqttCenter.MessageProcess;
using FantasyHomeCenter.Application.RoomCenter;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Receiving;
using MQTTnet.Server;
using Newtonsoft.Json;
using StackExchange.Profiling.Internal;

namespace FantasyHomeCenter.Application.MqttCenter;

public class MqttServerInstance
{
    private readonly IMemoryCache distributedCache;
    private readonly IDeviceService deviceService;

    private readonly string mqttClientKey = "mqtt_clients";

    private IMqttServer server;

    private int demoindex = 1;

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
      
            var list=  this.distributedCache.Get<List<MqttTopicOutput>>(this.mqttTopicKey);
            if (list.Any(x=>x.ClientId==s.ClientId&&x.Topic==s.TopicFilter))
            {
                var x = list.First(x => x.ClientId == s.ClientId && x.Topic == s.TopicFilter);
                list.Remove(x);

                this.distributedCache.Set(this.mqttTopicKey,  list);
            }
        });

        this.server.ClientSubscribedTopicHandler = new MqttServerClientSubscribedTopicHandlerDelegate(s =>
        {
            string clientid = s.ClientId;
            string topic = s.TopicFilter.Topic;
            var content= this.distributedCache.Get<List<MqttTopicOutput>>(this.mqttTopicKey);
            if (content==null||content.Count==0)
            {
                List<MqttTopicOutput> list = new();
                list.Add(new MqttTopicOutput(topic,clientid));
                this.distributedCache.Set(this.mqttTopicKey,  list);
            }
            else
            {
                var list=  this.distributedCache.Get<List<MqttTopicOutput>>(this.mqttTopicKey);
                if (!list.Any(x=>x.ClientId==clientid&&x.Topic==topic))
                {
                    list.Add(new MqttTopicOutput(topic,clientid));
                   
                 
                    this.distributedCache.Set(this.mqttTopicKey,  list);
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
            var content= this.distributedCache.Get<  List<MqttClientOuput>>(this.mqttClientKey);
            if (content==null||content.Count==0)
            {
                List<MqttClientOuput> list = new();
                list.Add(new MqttClientOuput(clientid,endpoint));
                this.distributedCache.Set(this.mqttClientKey,  list);
            }
            else
            {
                var list=  this.distributedCache.Get<  List<MqttClientOuput>>(this.mqttClientKey);
                if (!list.Any(x=>x.ClientId==clientid))
                {
                    list.Add(new MqttClientOuput(clientid,endpoint));
                   
                 
                     this.distributedCache.Set(this.mqttClientKey,  list.ToJson());
                }
            }

        }); 


        server.ClientDisconnectedHandler = new MqttServerClientDisconnectedHandlerDelegate(s =>
        {

            string clientid = s.ClientId;
            string endpoint = s.Endpoint;
            var content= this.distributedCache.Get<List<MqttClientOuput>>(this.mqttClientKey);
            if (content!=null&&content.Count!=0)
            {
              
                if (content.Any(x=>x.ClientId==clientid))
                {
                    content.Remove(content.First(y => y.ClientId == clientid));
                    string ser= JsonConvert.SerializeObject(content);
                     this.distributedCache.Set(this.mqttClientKey, content);
                }
            }

        });
        
        server.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(s =>
        {


            if (s.ApplicationMessage.Payload == null)
            {
               

                ControlUiMessageParser controlUiMessageParser =
                    new ControlUiMessageParser(s.ApplicationMessage.Topic, new MqttSendInfo(), this.deviceService, this.server);

                RoomListParser roomListParser = new RoomListParser(s.ApplicationMessage.Topic, new MqttSendInfo(), App.GetService<IRoomService>(), this);
                controlUiMessageParser.Next = roomListParser;

                controlUiMessageParser.Process();


            }
            else
            {
                string content = Encoding.UTF8.GetString(s.ApplicationMessage.Payload);

                MqttSendInfo info = JsonConvert.DeserializeObject<MqttSendInfo>(content);

                ControlUiMessageParser controlUiMessageParser =
                    new ControlUiMessageParser(s.ApplicationMessage.Topic, info, this.deviceService, this.server);

                RoomListParser roomListParser = new RoomListParser(s.ApplicationMessage.Topic, info, App.GetService<IRoomService>(), this);
                controlUiMessageParser.Next = roomListParser;

                controlUiMessageParser.Process();

            }




        });

        demoindex++;
        await server.StartAsync(serverOptions.Build());

    }

    public async Task PublishAsync(string topic, MqttSendInfo data)
    {
        string sendcontent = JsonConvert.SerializeObject(data);
       var res=  await this.server.PublishAsync(new MqttApplicationMessage
        {
            Topic=topic,
            Payload= Encoding.UTF8.GetBytes(sendcontent)
        });
        var resdata = res;
    }

    private IRepository<DeviceType> deviceTypeRepository;
    private IPluginService pluginService;
    public MqttServerInstance()
    {
        this.distributedCache =App.GetService<IMemoryCache>();

        this.deviceTypeRepository = App.GetService<IRepository<DeviceType>>();
        this.deviceService = App.GetService<IDeviceService>();
        this.pluginService = App.GetService<IPluginService>();
    }

    
        
    
    
    
}
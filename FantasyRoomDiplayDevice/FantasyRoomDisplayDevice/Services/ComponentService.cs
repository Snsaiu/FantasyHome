using DevExpress.Xpf.LayoutControl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

using FantasyHome.Application;

using FantasyHomeCenter.DevicePluginInterface;
using FantasyRoomDisplayDevice.Models;
using MQTTnet.Packets;

using MQTTnet;

using Newtonsoft.Json;
using CommandType = FantasyHomeCenter.DevicePluginInterface.CommandType;

namespace FantasyRoomDisplayDevice.Services
{
    public class ComponentService:IComponentService
    {
        private readonly PluginService pluginService;
        private readonly TempConfigService tempConfigService;
        private readonly ILogger logger;
        private readonly MqttService mqttService;

        public ComponentService(PluginService pluginService, TempConfigService tempConfigService,ILogger logger,MqttService mqttService)
        {
            this.pluginService = pluginService;
            this.tempConfigService = tempConfigService;
            this.logger = logger;
            this.mqttService = mqttService;
        }
        public ResultBase<List<ControlUI>> ParsePluginAndGetControlUI()
        {

            List<ControlUI> result = new List<ControlUI>(); 

            var plist = this.pluginService.DevicesControllers.ToList();
            List<MqttTopicFilter> filters = new List<MqttTopicFilter>();
            foreach (Lazy<DeviceControllerBase> item in plist)
            {
                var topic = new MqttTopicFilter() { Topic = item.Value.Key };
                filters.Add(topic);
                this.tempConfigService.DeviceMqttTopicFilters.Add(topic);
                this.logger.Info($"添加订阅主题:{item.Value.Key}");
            }
            //添加全局的订阅

            filters.Add(new MqttTopicFilter() { Topic = "fantasyhome-room-list" });
            filters.Add(new MqttTopicFilter() { Topic = "fantasyhome-room-add" });
            filters.Add(new MqttTopicFilter() { Topic = "fantasyhome-room-remove" });
            //filters.Add(new MqttTopicFilter() { Topic = "fantasyhome-ui-update" });
            filters.Add(new MqttTopicFilter() { Topic = "fantasyhome-restart" });

            this.tempConfigService.MqttTopicFilters = filters;
            this.mqttService.SubscriptionAsync(filters);


            foreach (Lazy<DeviceControllerBase> item in this.pluginService.DevicesControllers.ToList())
            {
                var device = this.tempConfigService.DevicePluginMetaOutputs.Where(x => x.Key == item.Value.Key).FirstOrDefault();

                if (device != null)
                {
                    this.logger.Info($"开始添加{device.Key}的组件");
                    foreach (var deviceMeta in device.Devices)
                    {

                        var control = item.Value.GetControlUi(deviceMeta);
                        if (control != null && control is ControlUI uc)
                        {
                            this.logger.Info($"添加设备组件:{deviceMeta.Name}");
                            uc.Topic = item.Value.Key;
                            uc.DeviceTypeKey = device.Key;
                            uc.RoomName = deviceMeta.Room.RoomName;
                            uc.DeviceKey = deviceMeta.Name;
                            var initdata = uc.BuildInitRequest();
                            uc.MqttMessageSendEvent += (con =>
                            {



                                MqttSendInfo info = new MqttSendInfo();
                                info.PluginKey = item.Value.Key;
                                info.Data = con.Data;

                                if (con.CommandType == CommandType.Get)
                                {
                                    info.CommandType = Models.CommandType.Get;
                                }
                                else
                                {
                                    info.CommandType = Models.CommandType.Set;
                                }
                                info.DeviceName = deviceMeta.Name;
                                info.Topic = uc.Topic;
                                string d = JsonConvert.SerializeObject(info);

                                var bytes = Encoding.UTF8.GetBytes(d);

                                this.logger.Info($"{info.DeviceName}发送消息:{d}");

                                this.mqttService.SendInfo(new MqttApplicationMessage()
                                {
                                    Topic = "fantasyhome-ui-receive",
                                    Payload = bytes
                                });
                            });
                            uc.SendMessage(new MessageModel() { Data = initdata, CommandType = CommandType.Get });
                            //Tile t = new Tile();
                            //t.Width = uc.Width;
                            //t.Height = uc.Height;
                            //t.Content = uc;
                            this.tempConfigService.Components.Add(uc);
                            result.Add(uc);
                        }
                    }
                }

                // var control= item.Value.GetDeskTopControlUi();
                // if (control!=null&&control is ControlUI uc)
                // {
                //     Tile t = new Tile();
                //     t.Width = uc.Width;
                //     t.Height = uc.Height;
                //     t.Content = uc;
                //     this.Tiles.Add(t);
                // }
            }

            return new ResultBase<List<ControlUI>>{Data=result,Succeeded=true};
        }
    }
}
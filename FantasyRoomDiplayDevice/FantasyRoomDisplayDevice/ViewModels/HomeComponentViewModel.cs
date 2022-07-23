using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Xpf.LayoutControl;
using DevExpress.XtraRichEdit.Import.Html;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyRoomDisplayDevice.Models;
using FantasyRoomDisplayDevice.Services;
using MQTTnet;
using MQTTnet.Packets;
using Newtonsoft.Json;
using CommandType = FantasyHomeCenter.DevicePluginInterface.CommandType;


namespace FantasyRoomDisplayDevice.ViewModels
{
    [ObservableObject]
    public partial class HomeComponentViewModel
    {
        private readonly PluginService pluginService;
        private readonly MqttService mqttService;
        private readonly TempConfigService tempConfigService;
        private readonly ILogger logger;

        [ObservableProperty]
        private ObservableCollection<Tile> tiles;

        public HomeComponentViewModel(PluginService pluginService,MqttService mqttService,TempConfigService tempConfigService,ILogger logger)
        {
            this.Tiles = new ObservableCollection<Tile>();
            this.pluginService = pluginService;
            this.mqttService = mqttService;
            this.tempConfigService = tempConfigService;
            this.logger = logger;

            //this.mqttService.MessageReceivedEvent += (data) =>
            //{

            //  string content=  Encoding.UTF8.GetString(data.Payload);

            //  MqttSendInfo info = JsonConvert.DeserializeObject<MqttSendInfo>(content);
            //  this.logger.Info($"来自mqtt的消息通知:{content}");
            // ControlUI device=  this.tempConfigService.Components.Where(x => x.DeviceTypeKey == info.PluginKey&&x.DeviceKey==info.DeviceName).FirstOrDefault();
            // if (device!=null)
            // {

            //     App.Current.Dispatcher.Invoke(() =>
            //     {
            //         this.logger.Info($"开始更新设备组件{info.DeviceName}的数据");
            //         device.UpdateState(info.Data);
            //         this.logger.Info($"更新设备组件{info.DeviceName}的数据已完成");
            //     });
                 
               
            // }



            //};


            //var plist= this.pluginService.DevicesControllers.ToList();
            //List<MqttTopicFilter> filters = new List<MqttTopicFilter>();
            //foreach (Lazy<IDeviceController> item in plist)
            //{
            //    filters.Add(new MqttTopicFilter(){Topic = item.Value.Key});
            //    this.logger.Info($"添加订阅主题:{item.Value.Key}");
            //}

            //this.tempConfigService.MqttTopicFilters = filters;
            //this.mqttService.SubscriptionAsync(filters);
            
            
            //foreach (Lazy<IDeviceController> item in this.pluginService.DevicesControllers.ToList())
            //{
            //    var device=  this.tempConfigService.DevicePluginMetaOutputs.Where(x => x.Key == item.Value.Key).FirstOrDefault();
                    
            //   if (device!=null)
            //   {
            //       this.logger.Info($"开始添加{device.Key}的组件");
            //       foreach ( var deviceMeta in device.Devices)
            //       {
                        
            //          var control = item.Value.GetDeskTopControlUi(deviceMeta);
            //          if (control!=null&&control is ControlUI uc)
            //          {
            //              this.logger.Info($"添加设备组件:{deviceMeta.Name}");
            //              uc.Topic = item.Value.Key;
            //              uc.DeviceTypeKey = device.Key;
            //              uc.DeviceKey = deviceMeta.Name;
            //              var initdata=   uc.BuildInitRequest();
            //            uc.MqttMessageSendEvent += (con =>
            //            {
                            
                           
                            
            //                MqttSendInfo info = new MqttSendInfo();
            //                info.PluginKey = item.Value.Key;
            //                info.Data = con.Data;

            //                if (con.CommandType==CommandType.Get)
            //                {
            //                    info.CommandType = Models.CommandType.Get;
            //                }
            //                else
            //                {
            //                    info.CommandType = Models.CommandType.Set;
            //                }
            //                info.DeviceName = deviceMeta.Name;
            //                info.Topic=uc.Topic;
            //                string d= JsonConvert.SerializeObject(info);

            //                var bytes= Encoding.UTF8.GetBytes(d);
                            
            //                this.logger.Info($"{info.DeviceName}发送消息:{d}");
                            
            //                this.mqttService.SendInfo(new MqttApplicationMessage()
            //                {
            //                    Topic = this.tempConfigService.MqttServiceTopic,
            //                    Payload = bytes
            //                });
            //            });
            //            uc.SendMessage(new MessageModel(){Data = initdata,CommandType = CommandType.Get});
            //              Tile t = new Tile();
            //              t.Width = uc.Width;
            //              t.Height = uc.Height;
            //              t.Content = uc;
            //              this.Tiles.Add(t);
            //              this.tempConfigService.Components.Add(uc);
            //          }
            //       }
            //   }

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
           
        }
    }


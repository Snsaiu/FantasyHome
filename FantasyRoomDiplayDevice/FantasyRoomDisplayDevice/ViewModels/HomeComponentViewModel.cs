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

        [ObservableProperty]
        private ObservableCollection<Tile> tiles;

        public HomeComponentViewModel(PluginService pluginService,MqttService mqttService,TempConfigService tempConfigService)
        {
            this.Tiles = new ObservableCollection<Tile>();
            this.pluginService = pluginService;
            this.mqttService = mqttService;
            this.tempConfigService = tempConfigService;

            this.mqttService.MessageReceivedEvent += (data) =>
            {

              string content=  Encoding.UTF8.GetString(data.Payload);

              MqttSendInfo info = JsonConvert.DeserializeObject<MqttSendInfo>(content);
             ControlUI device=  this.tempConfigService.Components.Where(x => x.DeviceTypeKey == info.PluginKey&&x.DeviceKey==info.DeviceName).FirstOrDefault();
             if (device!=null)
             {
                 device.UpdateState(info.Data);
             }



            };

            foreach (Lazy<IDeviceController> item in this.pluginService.DevicesControllers.ToList())
            {

               var device=  this.tempConfigService.DevicePluginMetaOutputs.Where(x => x.Key == item.Value.Key).FirstOrDefault();

               if (device!=null)
               {
                   
                   foreach ( var deviceMeta in device.Devices)
                   {

                      var control = item.Value.GetDeskTopControlUi(deviceMeta);
                      if (control!=null&&control is ControlUI uc)
                      {
                          uc.DeviceTypeKey = device.Key;
                          uc.DeviceKey = deviceMeta.Name;
                          var initdata=   uc.BuildInitRequest();
                        uc.MqttMessageSendEvent += (con =>
                        {
                            
                            
                            MqttSendInfo info = new MqttSendInfo();
                            info.PluginKey = item.Value.Key;
                            info.Data = con.Data;

                            if (con.CommandType==CommandType.Get)
                            {
                                info.CommandType = Models.CommandType.Get;
                            }
                            else
                            {
                                info.CommandType = Models.CommandType.Set;
                            }
                            info.DeviceName = deviceMeta.Name;
                            
                            string d= JsonConvert.SerializeObject(info);

                            var bytes= Encoding.UTF8.GetBytes(d);
                            this.mqttService.SendInfo(new MqttApplicationMessage()
                            {
                                Topic = this.tempConfigService.MqttServiceTopic,
                                Payload = bytes
                            });
                        });
                        uc.SendMessage(new MessageModel(){Data = initdata,CommandType = CommandType.Get});
                          Tile t = new Tile();
                          t.Width = uc.Width;
                          t.Height = uc.Height;
                          t.Content = uc;
                          this.Tiles.Add(t);
                          this.tempConfigService.Components.Add(uc);
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
           
        }
    }
}

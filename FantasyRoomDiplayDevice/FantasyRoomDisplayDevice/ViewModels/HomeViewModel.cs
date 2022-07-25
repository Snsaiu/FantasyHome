
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyRoomDisplayDevice.Services;
using FantasyRoomDisplayDevice.Views;
using MQTTnet;
using MQTTnet.Packets;
using Prism.Regions;
using System.Collections.ObjectModel;
using DevExpress.Xpf.WindowsUI;
using DevExpress.Xpf.WindowsUI.Internal;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyRoomDisplayDevice.Models;
using Newtonsoft.Json;
using FantasyRoomDisplayDevice.MqttMessageParse;
using Prism.Events;

namespace FantasyRoomDisplayDevice.ViewModels
{
    [ObservableObject]
    public partial class HomeViewModel:INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly MqttService mqttService;
        private readonly PluginService pluginService;
        private readonly TempConfigService tempConfigService;
        private readonly ILogger logger;
        private readonly IComponentService componentService;
        private readonly IEventAggregator eventAggregator;

        [ObservableProperty]
        private ObservableCollection<HamburgerMenuNavigationButton> rooms;

        public HomeViewModel(IRegionManager regionManager,MqttService mqttService,PluginService pluginService,TempConfigService tempConfigService,ILogger logger,IComponentService componentService,IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.mqttService = mqttService;
            this.pluginService = pluginService;
            this.tempConfigService = tempConfigService;
            this.logger = logger;
            this.componentService = componentService;
            this.eventAggregator = eventAggregator;
            this.Rooms = new ObservableCollection<HamburgerMenuNavigationButton>();

        }

        /// <summary>
        /// mqtt 接收
        /// </summary>
        private void mqttReceive()
        {

           


            this.mqttService.MessageReceivedEvent += (data) =>
            {
                string content = Encoding.UTF8.GetString(data.Payload);

                MqttSendInfo info = JsonConvert.DeserializeObject<MqttSendInfo>(content);
                this.logger.Info($"来自mqtt的消息通知:{content}");
                RoomListParser roomListParser = new RoomListParser(data.Topic, info,this.Rooms,this.eventAggregator);
              
                RoomAddParser roomAddParser = new RoomAddParser(data.Topic, info, this.Rooms,this.eventAggregator);
                roomListParser.Next = roomAddParser;

                ControlUIUpdateParser controlUIUpdateParser =
                    new ControlUIUpdateParser(info.Topic, info, this.tempConfigService);
                roomAddParser.Next=controlUIUpdateParser;

                RoomRemoveParser roomRemoveParser =
                    new RoomRemoveParser(data.Topic, info, this.Rooms, this.eventAggregator);

                controlUIUpdateParser.Next=roomRemoveParser;

                RestartAppParser restartAppParser = new RestartAppParser(data.Topic, info);
                roomRemoveParser.Next=restartAppParser;

                roomListParser.Process();







                //ControlUI device = this.tempConfigService.Components.Where(x => x.DeviceTypeKey == info.PluginKey && x.DeviceKey == info.DeviceName).FirstOrDefault();
                //if (device != null)
                //{
                //    App.Current.Dispatcher.Invoke(() =>
                //    {
                //        this.logger.Info($"开始更新设备组件{info.DeviceName}的数据");
                //        device.UpdateState(info.Data);
                //        this.logger.Info($"更新设备组件{info.DeviceName}的数据已完成");
                //    });


                //}
            };
        }

        [ICommand]
        private async Task  Loaded()
        {
            this.logger.Info("准备启动mqtt服务");
           var connectResult=await this.mqttService.StartAsync();

           if (connectResult.Succeeded == false)
           {
               this.logger.Error("mqtt服务启动失败");
               MessageBox.Show(connectResult.Errors.ToString());
           }
           this.logger.Info("mqtt服务启动成功");
           this.componentService.ParsePluginAndGetControlUI();
           mqttReceive();
            // 获得房间信息
           await this.mqttService.SendInfo(new MqttApplicationMessage()
            {
                Topic = "room-list-receive",
            });



          // await this.mqttService.SubscriptionAsync(new MqttTopicFilter()
          //  {
          //      Topic = this.tempConfigService.MqttServiceTopic
          //
          //  });
          this.regionManager.RequestNavigate("ItemRegion",source: nameof(HomeComponent));
        }

        private void rigRooms()
        {

        }

       
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("data"))
            {
                
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }
    }
}

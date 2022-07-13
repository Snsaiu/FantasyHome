
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

namespace FantasyRoomDisplayDevice.ViewModels
{
    [ObservableObject]
    public partial class HomeViewModel 
    {
        private readonly IRegionManager regionManager;
        private readonly MqttService mqttService;

        public HomeViewModel(IRegionManager regionManager,MqttService mqttService)
        {
            this.regionManager = regionManager;
            this.mqttService = mqttService;
          

        }

        [ICommand]
        private async Task  Loaded()
        {
           var connectResult=await this.mqttService.StartAsync();

           if (connectResult.Succeeded == false)
           {
               MessageBox.Show(connectResult.Errors.ToString());
           }

          await this.mqttService.SubscriptionAsync(new MqttTopicFilter()
           {
               Topic = "air"

           });
         await  this.mqttService.SendInfo(new MqttApplicationMessage()
               { Topic = "air", Payload = Encoding.UTF8.GetBytes("消息内容"), });
            this.regionManager.RequestNavigate("ItemRegion", nameof(HomeComponent));
        }

        [ICommand]
        private void OpenComponentPage(object obj)
        {
            string pageName = obj.ToString();
            switch (pageName)
            {
                case "主页":
                    this.regionManager.RequestNavigate("ItemRegion",nameof(HomeComponentViewModel));
                    break;
                default:
                    break;
            }
        }

    }
}

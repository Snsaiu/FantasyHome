using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Xpf.LayoutControl;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyRoomDisplayDevice.Services;


namespace FantasyRoomDisplayDevice.ViewModels
{
    [ObservableObject]
    public partial class HomeComponentViewModel
    {
        private readonly PluginService pluginService;
        private readonly MqttService mqttService;

        [ObservableProperty]
        private ObservableCollection<Tile> tiles;

        public HomeComponentViewModel(PluginService pluginService,MqttService mqttService)
        {
            this.Tiles = new ObservableCollection<Tile>();
            this.pluginService = pluginService;
            this.mqttService = mqttService;

            foreach (Lazy<IDeviceController> item in this.pluginService.DevicesControllers.ToList())
            {
                 var control= item.Value.GetDeskTopControlUi(this.mqttService.MessageProcesser);
                 if (control!=null&&control is UserControl uc)
                 {
                     Tile t = new Tile();
                     t.Width = uc.Width;
                     t.Height = uc.Height;
                     t.Content = uc;
                     this.Tiles.Add(t);
                 }
            }
           
        }
    }
}

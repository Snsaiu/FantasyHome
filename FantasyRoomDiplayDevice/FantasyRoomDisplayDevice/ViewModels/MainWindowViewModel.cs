
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyRoomDisplayDevice.Services;
using FantasyRoomDisplayDevice.Views;

using Prism.Mvvm;
using Prism.Regions;

namespace FantasyRoomDisplayDevice.ViewModels
{

    [ObservableObject]
    public partial class MainWindowViewModel 
    {
        private readonly IRegionManager regionManager;
        private readonly MqttService mqttService;

        public MainWindowViewModel( IRegionManager regionManager ,MqttService mqttService)
        {
            this.regionManager = regionManager;
            this.mqttService = mqttService;
        }

        /// <summary>
        /// 初始化结束
        /// </summary>
        [ICommand]
        private void Loaded()
        {
            this.regionManager.RequestNavigate("ContentRegion", nameof(Login));
        }

   
    }
}

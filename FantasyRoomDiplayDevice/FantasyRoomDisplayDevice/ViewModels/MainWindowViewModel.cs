
using FantasyRoomDisplayDevice.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Prism.Mvvm;
using Prism.Regions;

namespace FantasyRoomDisplayDevice.ViewModels
{

    [ObservableObject]
    public partial class MainWindowViewModel 
    {
        private readonly IRegionManager regionManager;

        public MainWindowViewModel( IRegionManager regionManager )
        {
            this.regionManager = regionManager;
           
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

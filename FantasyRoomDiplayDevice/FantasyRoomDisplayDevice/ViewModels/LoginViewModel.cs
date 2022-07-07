using FantasyRoomDisplayDevice.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Prism.Regions;

namespace FantasyRoomDisplayDevice.ViewModels
{
    [ObservableObject]
    public partial class LoginViewModel:INavigationAware
    {
        private readonly IRegionManager regionManager;

        public LoginViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        /// <summary>
        /// 登录
        /// </summary>
        [ICommand]
        private void Login()
        {
            this.regionManager.RequestNavigate("ContentRegion", nameof(Home));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
      
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
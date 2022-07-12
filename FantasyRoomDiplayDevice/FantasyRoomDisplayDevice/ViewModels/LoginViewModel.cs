using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyRoomDisplayDevice.Models;
using FantasyRoomDisplayDevice.Services;
using FantasyRoomDisplayDevice.Views;
using Microsoft.Extensions.Configuration;
using Prism.Regions;

namespace FantasyRoomDisplayDevice.ViewModels
{
    [ObservableObject]
    public partial class LoginViewModel:INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IConfiguration configuration;
        private readonly ICommonService commonService;

        public LoginViewModel(IRegionManager regionManager,IConfiguration configuration,ICommonService commonService)
        {
            this.regionManager = regionManager;
            this.configuration = configuration;
            this.commonService = commonService;
            this.tryConnectApiServer();
        }

        private void tryConnectApiServer()
        {
            var config= this.configuration.Get<Config>();
           if (string.IsNullOrEmpty(config.ApiServer.Host) == false && string.IsNullOrEmpty(config.ApiServer.Port)==false)
           {
               // try to connect server;
              bool connectResult= this.commonService.TryConnectTest(new HttpOptionInput()
                   { Host = config.ApiServer.Host, Port = config.ApiServer.Port });
              if (connectResult)
              {
                  //send curren machine guid code
              }
              
                  
               
           }
        }

        private bool tryConnectServer(string host, string port)
        {
            return false;
            
        }


        /// <summary>
        /// host
        /// </summary>
        [ObservableProperty]
        [AlsoNotifyCanExecuteFor((nameof(LoginCommand)))]
        private string host;

        /// <summary>
        /// port
        /// </summary>
        [ObservableProperty]
      [AlsoNotifyCanExecuteFor(nameof(LoginCommand))]
        private string port;
        
        /// <summary>
        /// 登录
        /// </summary>
        [ICommand(CanExecute = nameof(canLogin))]
        private void Login()
        {
          
            this.regionManager.RequestNavigate("ContentRegion", nameof(Home));
        }

        private bool canLogin()
        {
            if (string.IsNullOrEmpty(this.Host)||string.IsNullOrEmpty(this.Port))
            {
                return false;
            }

            return true;
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
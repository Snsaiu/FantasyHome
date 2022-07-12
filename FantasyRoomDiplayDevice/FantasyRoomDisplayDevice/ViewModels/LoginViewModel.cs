using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyHome.Application.Dto;
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
                  RegistMachineInput input = new RegistMachineInput();
                  input.ValidateUserName = config.UserInfo.UserName;
                  input.ValidateUsePassword = config.UserInfo.Pwd;
                  var res= this.commonService.Regist(input);
                  if (res.Succeeded)
                  {
                      this.regionManager.RequestNavigate("ContentRegion", nameof(Home));
                  }
                  else
                  {
                      MessageBox.Show(res.Errors.ToString());
                  }
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

        [ObservableProperty]
        [AlsoNotifyCanExecuteFor(nameof(LoginCommand))]
        private string userName;

        [ObservableProperty]
        [AlsoNotifyCanExecuteFor(nameof(LoginCommand))]
        private string pwd;
        
        /// <summary>
        /// 登录
        /// </summary>
        [ICommand(CanExecute = nameof(canLogin))]
        private void Login()
        {
            bool connectResult= this.commonService.TryConnectTest(new HttpOptionInput()
                { Host = this.Host,Port = this.Port});
            if (connectResult==false)
            {
                MessageBox.Show("无法连接服务器！可能是服务器地址错误");
                return;
            }
            
            RegistMachineInput input = new RegistMachineInput();
            input.ValidateUserName = this.UserName;
            input.ValidateUsePassword = this.Pwd;
            input.Host=this.Host;
            input.Port = this.Port;
            var res= this.commonService.Regist(input);
            if (res.Succeeded)
            {
                this.regionManager.RequestNavigate("ContentRegion", nameof(Home));
            }
            else
            {
                MessageBox.Show(res.Errors.ToString());
            }
            
          
          
        }

        private bool canLogin()
        {
            if (string.IsNullOrEmpty(this.Host)||string.IsNullOrEmpty(this.Port)||string.IsNullOrEmpty(this.UserName)||string.IsNullOrEmpty(this.Pwd))
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
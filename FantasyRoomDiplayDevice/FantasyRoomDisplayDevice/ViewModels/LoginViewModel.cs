﻿using System.Linq;
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
        private readonly PluginService pluginService;
        private readonly IDeviceService deviceService;
        private readonly TempConfigService tempConfigService;

        public LoginViewModel(IRegionManager regionManager,IConfiguration configuration,ICommonService commonService,
            PluginService pluginService,
            IDeviceService deviceService,
            TempConfigService tempConfigService
            )
        {
            this.regionManager = regionManager;
            this.configuration = configuration;
            this.commonService = commonService;
            this.pluginService = pluginService;
            this.deviceService = deviceService;
            this.tempConfigService = tempConfigService;
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
                      var downloadRes= this.deviceService.DownloadPlugins();

                      if (downloadRes.Succeeded==false)
                      {
                        
                          MessageBox.Show(downloadRes.Errors.ToString());
                          return;

                      }
                      
                      this.pluginService.LoadPlugins();
                    
                      NavigationParameters param = new();
                      param.Add("data",downloadRes.Data);
                      this.tempConfigService.DevicePluginMetaOutputs = downloadRes.Data;
                      this.regionManager.RequestNavigate("ContentRegion", nameof(Home),param);
                      return;
                      
                  }
                  else
                  {
                      MessageBox.Show(res.Errors.ToString());
                  }
                  //send curren machine guid code
              }
              
                  
               
           }
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


        [ICommand]
        private void Loaded()
        {
            
            this.tryConnectApiServer();
        }
        
        
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
                var downloadRes= this.deviceService.DownloadPlugins();

                if (downloadRes.Succeeded==false)
                {
                        
                    MessageBox.Show(downloadRes.Errors.ToString());
                    return;

                }
                this.pluginService.LoadPlugins();
                
                NavigationParameters param = new();
                param.Add("data",downloadRes.Data);
                this.tempConfigService.DevicePluginMetaOutputs = downloadRes.Data;
                this.regionManager.RequestNavigate("ContentRegion", nameof(Home),param);
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
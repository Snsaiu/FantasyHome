using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyHome.Application.Dto;
using FantasyRoomDisplayDevice.Models;
using FantasyRoomDisplayDevice.Services;
using FantasyRoomDisplayDevice.Views;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private readonly ILogger logger;

        public LoginViewModel(IRegionManager regionManager,IConfiguration configuration,ICommonService commonService,
            PluginService pluginService,
            IDeviceService deviceService,
            TempConfigService tempConfigService,
            ILogger logger
            )
        {
            this.regionManager = regionManager;
            this.configuration = configuration;
            this.commonService = commonService;
            this.pluginService = pluginService;
            this.deviceService = deviceService;
            this.tempConfigService = tempConfigService;
            this.logger = logger;
        }

        private void tryConnectApiServer()
        {
            this.logger.Info("启动获得配置文件");
            var config= this.configuration.Get<Config>();
            this.logger.Info($"获得配置文件完成,配置文件是{JsonConvert.SerializeObject(config)}");
           if (string.IsNullOrEmpty(config.ApiServer.Host) == false && string.IsNullOrEmpty(config.ApiServer.Port)==false)
           {
               // try to connect server;
               this.logger.Info("存在主机连接地址,开始尝试连接服务器");
              bool connectResult= this.commonService.TryConnectTest(new HttpOptionInput()
                   { Host = config.ApiServer.Host, Port = config.ApiServer.Port });
              if (connectResult)
              {
                  this.logger.Info("服务器连接成功,尝试注册设备到服务器");
                  RegistMachineInput input = new RegistMachineInput();
                  input.ValidateUserName = config.UserInfo.UserName;
                  input.ValidateUsePassword = config.UserInfo.Pwd;
                  var res= this.commonService.Regist(input);
                 
                  if (res.Succeeded)
                  {
                      this.logger.Info("注册设备成功，开始下载插件");
                      var downloadRes= this.deviceService.DownloadPlugins();

                      if (downloadRes.Succeeded==false)
                      {
                        this.logger.Error($"下载插件发生了错误。错误信息:{downloadRes.Errors.ToString()}");
                          MessageBox.Show(downloadRes.Errors.ToString());
                          return;

                      }
                      
                      this.logger.Info($"下载插件完成，尝试读取插件");
                      this.pluginService.LoadPlugins();
                    this.logger.Info("读取插件成功");
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
            this.logger.Info("尝试登录");
            bool connectResult= this.commonService.TryConnectTest(new HttpOptionInput()
                { Host = this.Host,Port = this.Port});
            if (connectResult==false)
            {
                this.logger.Error($"登录失败,无法连接服务器");
                MessageBox.Show("无法连接服务器！可能是服务器地址错误");
                return;
            }
            
          
            this.logger.Info("服务器连接成功,尝试注册设备到服务器");
            
            RegistMachineInput input = new RegistMachineInput();
            input.ValidateUserName = this.UserName;
            input.ValidateUsePassword = this.Pwd;
            input.Host=this.Host;
            input.Port = this.Port;
            var res= this.commonService.Regist(input);
            if (res.Succeeded)
            {   this.logger.Info("注册设备成功，开始下载插件");
                var downloadRes= this.deviceService.DownloadPlugins();

                if (downloadRes.Succeeded==false)
                {
                    this.logger.Error($"下载插件发生了错误。错误信息:{downloadRes.Errors.ToString()}");
                    MessageBox.Show(downloadRes.Errors.ToString());
                    return;

                }

                this.logger.Info($"下载插件完成，开始读取插件");
                this.pluginService.LoadPlugins();
                this.logger.Info($"读取插件完成");
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
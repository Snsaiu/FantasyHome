
using FantasyHome.Application;
using FantasyHome.Application.Dto;
using FantasyHome.Application.Impls;
using FantasyRoomDisplayDevice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FantasyRoomDisplayDevice.Services
{

    public class CommonService : ICommonService
    {
        private readonly TempConfigService tempConfigService;
        private readonly IConfiguration configuration;
        private readonly IConfigWriter configWriter;
        private ICommonApplication commonApplication = null;

        public CommonService(TempConfigService tempConfigService,IConfiguration configuration,IConfigWriter configWriter)
        {
            this.commonApplication=new CommonApplication();
            this.tempConfigService = tempConfigService;
            this.configuration = configuration;
            this.configWriter = configWriter;
        }

        public bool TryConnectTest(HttpOptionInput input)
        {
             ResultBase<string> resultBase = this.commonApplication.TestConnect(new TryConnectParamInput() { Host = input.Host, Port = input.Port });
            if (resultBase.Succeeded)
            {
                this.tempConfigService.Host = input.Host;
                this.tempConfigService.Port = input.Port;
                return true;
            }
            return false;
        }

 

        public ResultBase<bool> Regist(RegistMachineInput input)
        {
            if (string.IsNullOrEmpty( input.Host))
            {
                input.Host = this.tempConfigService.Host;
            
            }

            if (string.IsNullOrEmpty(input.Port))
            {
                input.Port = this.tempConfigService.Port;
            }

            string pwd = input.ValidateUsePassword;
            var res=  this.commonApplication.Regist(input);
            if (res.Succeeded)
            {
                this.tempConfigService.Token = res.Data.Token;
                this.tempConfigService.MqttHost = res.Data.MqttService;
                this.tempConfigService.MqttPort = res.Data.Port;
                this.tempConfigService.UserName = input.ValidateUserName;
                this.tempConfigService.Pwd = pwd;
                
                //保存到配置文件中
                var config= this.configuration.Get<Config>();
                config.UserInfo = new UserInfo();
                config.UserInfo.Pwd = pwd;
                config.UserInfo.UserName = this.tempConfigService.UserName;
                config.MqttServer = new();
                config.MqttServer.Host = this.tempConfigService.MqttHost;
                config.MqttServer.Port = this.tempConfigService.MqttPort;
                config.ApiServer = new ApiServer();
                config.ApiServer.Host = this.tempConfigService.Host;
                config.ApiServer.Port = this.tempConfigService.Port;
                string content = JsonConvert.SerializeObject(config);
              
                if (this.configWriter.Write(content))
                {
                    return new ResultBase<bool>() { Succeeded = true };
                }
               return new ResultBase<bool>(){Succeeded = false,Errors = "写入配置文件出错！"};
            }
            else
            {
                return new ResultBase<bool>() { Succeeded = false, Errors = res.Errors };
            }

        }
    }
}
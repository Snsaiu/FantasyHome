
using FantasyHome.Application;
using FantasyHome.Application.Dto;
using FantasyHome.Application.Impls;
using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.Services
{

    public class CommonService : ICommonService
    {
        private readonly TempConfigService tempConfigService;
        private ICommonApplication commonApplication = null;

        public CommonService(TempConfigService tempConfigService)
        {
            this.commonApplication=new CommonApplication();
            this.tempConfigService = tempConfigService;
          
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
                return new ResultBase<bool>(){Succeeded = true};
            }
            else
            {
                return new ResultBase<bool>() { Succeeded = false, Errors = res.Errors };
            }

        }
    }
}
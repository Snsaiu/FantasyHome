
using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.Services
{

    public class CommonService : ICommonService
    {
        // private ICommonApplication commonApplication = null;

        public CommonService()
        {
            // this.commonApplication = new CommonApplication();
        }

        public bool TryConnectTest(HttpOptionInput input)
        {
            // ResultBase<string> resultBase = this.commonApplication.TestConnect(new TryConnectParamInput() { Host = input.Host, Port = input.Port });
            //if (resultBase.Succeeded)
            //{
            //    return true;
            //}
            return false;
        }

        public bool SendMachineCode()
        {
            return true;
        }
    }
}
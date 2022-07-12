using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.Services
{

    public interface ICommonService
    {
        bool TryConnectTest(HttpOptionInput input);

        /// <summary>
        /// 发送机器码
        /// </summary>
        /// <returns></returns>
        bool SendMachineCode();
    }
}
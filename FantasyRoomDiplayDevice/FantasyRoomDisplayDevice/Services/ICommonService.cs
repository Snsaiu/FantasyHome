using FantasyHome.Application;
using FantasyHome.Application.Dto;
using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.Services
{

    public interface ICommonService
    {
        bool TryConnectTest(HttpOptionInput input);

     

        ResultBase<bool> Regist(RegistMachineInput input);

    }
}
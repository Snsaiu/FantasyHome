using FantasyHomeCenter.Application.ControlDeviceCenter.Dto;

namespace FantasyHomeCenter.Application.ControlDeviceCenter;

public interface IControlDeviceService
{
    /// <summary>
    /// 登录或者注册当前设备
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    RESTfulResult<RegistResultOutput> Regist(RegistMachineInput input);


}
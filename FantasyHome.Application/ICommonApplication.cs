using FantasyHome.Application.Dto;

namespace FantasyHome.Application;

public interface ICommonApplication
{

    /// <summary>
    /// 检查是否可以链接到服务器
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    ResultBase<string> TestConnect(TryConnectParamInput input);

    /// <summary>
    /// 注册或者登录设备
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    ResultBase<string> Regist(RegistMachineInput input);

}
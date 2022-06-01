using FantasyHomeCenter.Application.UserCenter.Dto;

namespace FantasyHomeCenter.Application.UserCenter;

public interface IUserService
{

    /// <summary>
    /// 用户授权
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public LoginUserOutput Author(LoginUserInput input);
}
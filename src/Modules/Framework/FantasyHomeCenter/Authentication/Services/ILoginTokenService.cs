// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using FantasyHomeCenter.Authentication.Dtos;
using FantasyHomeCenter.Base;

namespace FantasyHomeCenter.Authentication.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    public interface ILoginTokenService : IServiceBase<LoginTokenDto, Guid>
    {
    }
}

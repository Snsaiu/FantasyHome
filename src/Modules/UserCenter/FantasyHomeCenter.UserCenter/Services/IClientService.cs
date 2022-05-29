// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.UserCenter.Services
{
    /// <summary>
    /// 客户端服务
    /// </summary>
    public interface IClientService : IServiceBase<ClientDto, Guid>
    {
        /// <summary>
        /// 根据客户端编号获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<FunctionDto>> GetFunctions(Guid id);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TokenOutput> Login(ClientLoginInput input);

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        Task<TokenOutput> RefreshToken(RefreshTokenInput input);
    }
}

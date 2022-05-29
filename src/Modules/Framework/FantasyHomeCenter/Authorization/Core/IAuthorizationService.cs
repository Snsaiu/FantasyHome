// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;
using FantasyHomeCenter.Authentication.Dtos;
using FantasyHomeCenter.Authorization.Dtos;

namespace FantasyHomeCenter.Authorization.Core
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        Identity GetIdentity();
        /// <summary>
        /// 获取当前请求的功能点
        /// </summary>
        /// <returns></returns>
        Task<ApiEndpoint> GetApiEndpoint();
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        Task<bool> ChecktContenxtApiEndpoint();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetIdentityId();
    }
}
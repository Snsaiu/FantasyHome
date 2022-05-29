// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Authentication.Dtos;
using FantasyHomeCenter.Authentication.Services;
using FantasyHomeCenter.Client.Base;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    [ScopedService]
    public class LoginTokenService : ClientServiceBase<LoginTokenDto, Guid>, ILoginTokenService
    {
        public LoginTokenService(IApiCaller apiCaller) : base(apiCaller, "login-token")
        {
        }

        public Task<bool> CheckLoginIdUsable(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Disable(Guid id, bool isDisabled = true)
        {
            return await apiCaller.PutAsync<object, bool>($"{controller}/{id}/disable/{isDisabled}");
        }
    }
}

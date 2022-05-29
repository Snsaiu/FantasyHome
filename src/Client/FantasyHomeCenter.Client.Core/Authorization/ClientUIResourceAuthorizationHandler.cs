// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Client.Base.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientUIResourceAuthorizationHandler : AuthorizationHandler<ClientUIAuthorizationRequirement>
    {

        private readonly IAuthenticationStateManager authenticationStateManager;

        public ClientUIResourceAuthorizationHandler(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientUIAuthorizationRequirement requirement)
        {
            if (context.Resource is ClientResource resource)
            {
                bool state = false;

                state = resource.AndCondition ? true : false;

                foreach (string key in resource.Keys)
                {
                    var isAuth = await authenticationStateManager.CheckCurrentUserHaveBtnResourceKey(key);
                    if (resource.AndCondition)
                    {
                        if (!isAuth)
                        {
                            state = false;
                        }
                    }
                    else
                    {
                        if (isAuth)
                        {
                            state = true;
                        }
                    }
                }

                if (state)
                {
                    //如果当前用户有资源访问权限，则返回成功
                    context.Succeed(requirement);
                }
            }
        }
    }
}

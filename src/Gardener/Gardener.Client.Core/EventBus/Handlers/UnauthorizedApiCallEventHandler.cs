﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    [TransientService]
    public class UnauthorizedApiCallEventHandler : IEventHandler<UnauthorizedApiCallEvent>
    {

        private readonly IAuthenticationStateManager authenticationStateManager;

        public UnauthorizedApiCallEventHandler(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public async Task Handler(UnauthorizedApiCallEvent e)
        {
            await authenticationStateManager.ReloadCurrentUserInfos();
        }
    }
}

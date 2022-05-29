// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using FantasyHomeCenter.Email.Dtos;
using FantasyHomeCenter.Email.Services;
using System;

namespace Gardener.Email.Client.Services
{
    [ScopedService]
    public class EmailServerConfigService : ClientServiceBase<EmailServerConfigDto, Guid>, IEmailServerConfigService
    {
        public EmailServerConfigService(IApiCaller apiCaller) : base(apiCaller, "email-server-config")
        {
        }
    }
}

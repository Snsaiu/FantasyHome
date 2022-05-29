// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Email.Dtos;
using FantasyHomeCenter.Email.Services;
using System;

namespace FantasyHomeCenter.Email.Client.Services
{
    [ScopedService]
    public class EmailServerConfigService : ClientServiceBase<EmailServerConfigDto, Guid>, IEmailServerConfigService
    {
        public EmailServerConfigService(IApiCaller apiCaller) : base(apiCaller, "email-server-config")
        {
        }
    }
}

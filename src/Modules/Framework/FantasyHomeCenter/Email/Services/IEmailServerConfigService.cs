// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.Email.Dtos;

namespace FantasyHomeCenter.Email.Services
{
    /// <summary>
    /// 邮件服务器配置服务
    /// </summary>
    public interface IEmailServerConfigService : IServiceBase<EmailServerConfigDto,Guid>
    {

    }
}

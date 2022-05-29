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
    /// 邮件模板服务
    /// </summary>
    public interface IEmailTemplateService : IServiceBase<EmailTemplateDto, Guid>
    {
    }
}

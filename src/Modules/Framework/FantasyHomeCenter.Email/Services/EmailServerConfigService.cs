// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using FantasyHomeCenter.Email.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using FantasyHomeCenter.Email.Domains;
using Gardener;

namespace FantasyHomeCenter.Email.Services
{

    /// <summary>
    /// 邮件服务器配置服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailServerConfigService : ServiceBase<EmailServerConfig, EmailServerConfigDto,Guid>, IEmailServerConfigService
    {
        /// <summary>
        /// 邮件服务器配置服务
        /// </summary>
        /// <param name="repository"></param>
        public EmailServerConfigService(IRepository<EmailServerConfig> repository) : base(repository)
        {
        }
    }
}

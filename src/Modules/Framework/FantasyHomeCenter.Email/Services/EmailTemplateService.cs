// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using FantasyHomeCenter.Email.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FantasyHomeCenter.Email.Domains;
using FantasyHomeCenter;

namespace FantasyHomeCenter.Email.Services
{

    /// <summary>
    /// 邮件模板服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailTemplateService : ServiceBase<EmailTemplate, EmailTemplateDto, Guid>, IEmailTemplateService
    {

        private readonly IEmailService _emailService;

        /// <summary>
        /// 邮件模板服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="emailService"></param>
        public EmailTemplateService(IRepository<EmailTemplate> repository, IEmailService emailService) : base(repository)
        {
            _emailService = emailService;
        }
    }
}

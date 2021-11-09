﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Email.Dtos;
using System;
using System.Threading.Tasks;

namespace Gardener.Email.Services
{
    /// <summary>
    /// 邮件模板服务
    /// </summary>
    public interface IEmailTemplateService : IServiceBase<EmailTemplateDto, Guid>
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> SendTest(SendEmailInputDto input);
    }
}
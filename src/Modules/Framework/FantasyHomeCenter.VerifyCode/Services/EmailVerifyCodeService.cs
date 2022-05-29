﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using FantasyHomeCenter.Attributes;
using FantasyHomeCenter.VerifyCode.Core;
using FantasyHomeCenter.VerifyCode.Dtos;
using FantasyHomeCenter.VerifyCode.Enums;
using FantasyHomeCenter.VerifyCode.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FantasyHomeCenter.ImageVerifyCode.Services
{
    /// <summary>
    /// 邮件验证码服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailVerifyCodeService : IEmailVerifyCodeService, IDynamicApiController
    {
        private readonly IVerifyCode verifyCodeService;
        /// <summary>
        /// 验证码服务
        /// </summary>
        /// <param name="verifyCodeServiceProvider"></param>
        public EmailVerifyCodeService(Func<VerifyCodeTypeEnum, IVerifyCode> verifyCodeServiceProvider)
        {
            this.verifyCodeService = verifyCodeServiceProvider(VerifyCodeTypeEnum.Email);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input">类型</param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<EmailVerifyCodeOutput> Create(EmailVerifyCodeInput input)
        {
            EmailVerifyCodeOutput imageVerifyCode =(EmailVerifyCodeOutput) await verifyCodeService.Create(input);
            return imageVerifyCode;
        }
        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Remove(string key)
        {
            return await verifyCodeService.Remove(key);
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Verify(EmailVerifyCodeCheckInput input)
        {
            return await verifyCodeService.Verify(input.VerifyCode, input.VerifyCodeKey);
        }
    }
}

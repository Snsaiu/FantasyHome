// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using FantasyHomeCenter.Email.Enums;
using System;

namespace FantasyHomeCenter.VerifyCode.Core.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailVerifyCodeOptions: VerifyCodeOptions, IConfigurableOptions
    {
        /// <summary>
        /// 邮件模板编号
        /// </summary>
        public Guid EmailTemplateId { get; set; }
        /// <summary>
        /// 邮件服务器标签
        /// </summary>
        public EmailServerTag EmailServerTag { get; set; } = EmailServerTag.Base;
    }
}

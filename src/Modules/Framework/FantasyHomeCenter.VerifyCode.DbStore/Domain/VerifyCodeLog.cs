// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Attributes;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.VerifyCode.Enums;
using System;
using System.ComponentModel;

namespace FantasyHomeCenter.VerifyCode.DbStore.Domain
{
    /// <summary>
    /// 验证码
    /// </summary>
    [Description("验证码")]
    [IgnoreAudit]
    public class VerifyCodeLog : GardenerEntityBase<Guid>
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        [DisplayName("验证码类型")]
        public VerifyCodeTypeEnum VerifyCodeType { get; set; }
        /// <summary>
        /// 验证码唯一键
        /// </summary>
        [DisplayName("验证码唯一键")]
        public string Key { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [DisplayName("验证码")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTimeOffset EndTime { get; set; }
    }
}

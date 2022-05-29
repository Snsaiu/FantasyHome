// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using FantasyHomeCenter.VerifyCode.Enums;

namespace FantasyHomeCenter.VerifyCode.Dtos
{
    /// <summary>
    /// 邮件验证码返回结果
    /// </summary>
    [Description("邮件验证码返回结果")]
    public class EmailVerifyCodeOutput : VerifyCodeOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Email;
    }
}

// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.VerifyCode.Enums;

namespace FantasyHomeCenter.VerifyCode.Dtos
{
    /// <summary>
    /// 邮件验证码输入
    /// </summary>
    public class EmailVerifyCodeCheckInput : VerifyCodeCheckInput
    {
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Email;
    }
}

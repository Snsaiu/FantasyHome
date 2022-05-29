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
    /// 验证码返回结果
    /// </summary>
    public class VerifyCodeOutput
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public virtual VerifyCodeTypeEnum VerifyCodeType { get; }
        /// <summary>
        /// 验证码唯一键
        /// </summary>
        [DisplayName("验证码唯一键")]
        public string Key { get; set; }
    }
}

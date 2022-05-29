// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FantasyHomeCenter.VerifyCode.Enums;

namespace FantasyHomeCenter.VerifyCode.Dtos
{
    /// <summary>
    /// 邮件验证码输入
    /// </summary>
    public class EmailVerifyCodeInput : VerifyCodeInput
    {
        /// <summary>
        /// 接收方邮箱地址
        /// </summary>
        [DisplayName("接收方邮箱地址")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        [RegularExpression("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "请输入正确的邮件地址")]
        public string ToEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Email;
    }
}

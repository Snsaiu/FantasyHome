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
    /// 图片验证码返回结果
    /// </summary>
    [Description("图片验证码")]
    public class ImageVerifyCodeOutput : VerifyCodeOutput
    {
        /// <summary>
        /// Base64图片
        /// </summary>
        [DisplayName("Base64图片")]
        public string Base64Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Image;
    }
}

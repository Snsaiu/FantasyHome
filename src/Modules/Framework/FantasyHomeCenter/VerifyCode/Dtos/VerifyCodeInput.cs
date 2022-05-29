// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.VerifyCode.Enums;

namespace FantasyHomeCenter.VerifyCode.Dtos
{
    /// <summary>
    /// 验证码输入
    /// </summary>
    public abstract class VerifyCodeInput: VerifyCodeBase
    {
        /// <summary>
        /// 创建code参数
        /// 不穿时使用配置
        /// </summary>
        public CharacterCodeCreateParam CreateCodeParam { get; set; }
    }
}

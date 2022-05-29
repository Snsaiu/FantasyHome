// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace FantasyHomeCenter.Authentication.Enums
{
    /// <summary>
    /// 身份类型
    /// </summary>
    public enum IdentityType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown,
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        User,
        /// <summary>
        /// 客户端
        /// </summary>
        [Description("客户端")]
        Client
    }
}

// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace FantasyHomeCenter.NotificationSystem.Enums
{
    /// <summary>
    /// 用户在线状态
    /// </summary>
    public enum UserOnlineStatus
    {
        /// <summary>
        /// 上线
        /// </summary>
        [Description("上线")]
        Online,
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Offline
    }
}

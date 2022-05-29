// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.NotificationSystem.Dtos;
using FantasyHomeCenter.NotificationSystem.Enums;

namespace FantasyHomeCenter.NotificationSystem
{
    /// <summary>
    /// 用户在线状态变化通知数据
    /// </summary>
    public class UserOnlineChangeNotificationData : NotificationDataBase
    {
        /// <summary>
        /// 在线状态
        /// </summary>
        public UserOnlineStatus OnlineStatus { get; set; }
        
    }
}

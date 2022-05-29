// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.NotificationSystem.Dtos.Notification;

namespace FantasyHomeCenter.NotificationSystem.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    public interface IChatDemoService
    {
        /// <summary>
        /// 获取历史聊天记录
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChatNotificationData>> GetHistory();
    }
}

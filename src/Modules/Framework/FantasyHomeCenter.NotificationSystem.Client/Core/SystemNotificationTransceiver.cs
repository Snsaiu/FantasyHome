// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using FantasyHomeCenter.NotificationSystem.Dtos;

namespace FantasyHomeCenter.NotificationSystem.Client
{
    /// <summary>
    /// 系统通知收发器
    /// </summary>
    [ScopedService]
    public class SystemNotificationTransceiver
    {
        private readonly ISignalRClientManager _signalRClientManager;
        public SystemNotificationTransceiver(ISignalRClientManager signalRClientManager)
        {
            _signalRClientManager = signalRClientManager;
        }
        
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task Send(NotificationData notificationData)
        {
            return _signalRClientManager
                .Get(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientNames)
                .SendAsync("Send", notificationData);
        }
    }

}

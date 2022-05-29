// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.EventBus;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.NotificationSystem.Services;
using FantasyHomeCenter.EventBus;
using FantasyHomeCenter.NotificationSystem.Dtos;
using FantasyHomeCenter.NotificationSystem.Dtos.Notification;
using FantasyHomeCenter.NotificationSystem.Enums;

namespace FantasyHomeCenter.NotificationSystem.Core.Subscribes
{
    /// <summary>
    /// 系统通知事件订阅
    /// </summary>
    public class ChatNotificationDataEventSubscriber : IEventSubscriber, ISingleton
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public ChatNotificationDataEventSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 聊天数据
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.SystemNotify) + nameof(NotificationDataType.Chat))]
        public async Task Chat(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            EventInfo<NotificationData> eventInfo = (EventInfo<NotificationData>)eventSource.Payload;

            ChatNotificationData chatNotification = System.Text.Json.JsonSerializer.Deserialize<ChatNotificationData>(eventInfo.Data.Data);
            //收到聊天消息，转发给所有客户端
            await systemNotificationService.SendToAllClient(eventInfo.Data);
            ChatDemoService.AddChatMessage(chatNotification);
        }


    }
}

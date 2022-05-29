﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using FantasyHomeCenter.NotificationSystem.Dtos.Notification;
using FantasyHomeCenter.NotificationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.NotificationSystem.Client.Core.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    [ScopedService]
    public class ChatDemoService : IChatDemoService
    {
        private readonly string controller = "chat-demo";
        private readonly IApiCaller apiCaller;
        public ChatDemoService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }
        public async Task<IEnumerable<ChatNotificationData>> GetHistory()
        {
            return await apiCaller.GetAsync<IEnumerable<ChatNotificationData>>($"{controller}/history");
        }
    }
}

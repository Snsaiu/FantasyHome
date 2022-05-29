﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FantasyHomeCenter.EventBus
{
    /// <summary>
    /// 事件发送服务
    /// </summary>
    public class EventBusService : IEventBus
    {
        private readonly IEventPublisher eventPublisher;

        public EventBusService(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Publish<TEvent>(TEvent e, CancellationToken? cancellationToken) where TEvent : EventBase
        {
            EventSource<TEvent> eventSource = new EventSource<TEvent>(e.EventType.ToString() + e.EventGroup);
            eventSource.Body = e;
            if (cancellationToken.HasValue) 
            {
                eventSource.CancellationToken = cancellationToken.Value;
            }
            return eventPublisher.PublishAsync(eventSource);
        }

        public Subscriber Subscribe<TEvent>(Func<TEvent, Task> callBack) where TEvent : EventBase
        {
            throw new NotImplementedException();
        }

        public void UnSubscribe(Subscriber subscriber)
        {
            throw new NotImplementedException();
        }
    }
}

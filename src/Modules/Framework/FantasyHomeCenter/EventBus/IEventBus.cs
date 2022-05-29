// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace FantasyHomeCenter.EventBus
{ 
    /// <summary>
    /// 事件服务
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        Task Publish<TEvent>(TEvent e, CancellationToken? cancellationToken=null) where TEvent : EventBase;

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Subscriber Subscribe<TEvent>(Func<TEvent,Task> callBack) where TEvent : EventBase;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="subscriber"></param>
        void UnSubscribe(Subscriber subscriber);
    }
}

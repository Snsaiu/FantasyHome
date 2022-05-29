// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.EventBus;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Client.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IEventSubscriber<TEvent> where TEvent : EventBase
    {

        /// <summary>
        /// 判断是否需要过滤
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool Ignore(TEvent e)
        {
            if (e == null)
            { 
                return true;
            }
            return false;
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        Task CallBack(TEvent e);
    }
}

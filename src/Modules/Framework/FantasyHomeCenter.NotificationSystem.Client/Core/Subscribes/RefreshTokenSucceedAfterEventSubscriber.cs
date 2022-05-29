// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.EventBus.Events;

namespace FantasyHomeCenter.NotificationSystem.Client.Subscribes
{
    /// <summary>
    /// 刷新token后
    /// </summary>
    [TransientService]
    public class RefreshTokenSucceedAfterEventSubscriber : IEventSubscriber<RefreshTokenSucceedAfterEvent>
    {
        public Task CallBack(RefreshTokenSucceedAfterEvent e)
        {
            
            return Task.CompletedTask;
        }
    }
}

// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Client.Base.EventBus.Events;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Client.Core.Subscribes
{
    /// <summary>
    /// 重载用户后，连接系统通知
    /// </summary>
    [TransientService]
    public class ReloadCurrentUserEventSubscriber : IEventSubscriber<ReloadCurrentUserEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public ReloadCurrentUserEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public Task CallBack(ReloadCurrentUserEvent e)
        {
            return signalRClientManager.ConnectionAndStartAll();
        }
    }
}

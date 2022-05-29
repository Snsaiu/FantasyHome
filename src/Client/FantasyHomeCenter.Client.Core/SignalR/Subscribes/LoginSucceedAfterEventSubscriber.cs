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
    /// 登录成功后，连接系统通知
    /// </summary>
    [TransientService]
    public class LoginSucceedAfterEventSubscriber : IEventSubscriber<LoginSucceedAfterEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public LoginSucceedAfterEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public Task CallBack(LoginSucceedAfterEvent e)
        {
            return signalRClientManager.ConnectionAndStartAll();
        }
    }
}

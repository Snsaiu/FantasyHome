// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.EventBus;

namespace Gardener.Client.Base.EventBus.Events
{
    /// <summary>
    /// 重载当前用户事件
    /// </summary>
    public class ReloadCurrentUserEvent:EventBase
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
    }
}

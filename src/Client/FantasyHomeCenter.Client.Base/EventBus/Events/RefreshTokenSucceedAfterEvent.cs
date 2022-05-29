﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.EventBus;

namespace Gardener.Client.Base.EventBus.Events
{
    /// <summary>
    /// 刷新token成功后
    /// </summary>
    public class RefreshTokenSucceedAfterEvent:EventBase
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
    }
}

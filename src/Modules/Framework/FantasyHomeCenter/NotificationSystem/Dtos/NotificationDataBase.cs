// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;

namespace FantasyHomeCenter.NotificationSystem.Dtos
{
    /// <summary>
    /// 系统通知数据基类
    /// </summary>
    public class NotificationDataBase
    {
        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;
    }
}

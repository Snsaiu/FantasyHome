// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;

namespace FantasyHomeCenter.EntityFramwork.Event
{
    /// <summary>
    /// 保存数据更改事件
    /// </summary>
    public class FantasyHomeCenterDbContextSavingChangesEvent
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}

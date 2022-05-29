// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.NotificationSystem.Options
{
    /// <summary>
    /// 系统通知配置
    /// </summary>
    public class SystemNotificationHubOptions: IConfigurableOptions
    {
        /// <summary>
        /// 域
        /// </summary>
        public string[] Origins { get; set; }
    }
}

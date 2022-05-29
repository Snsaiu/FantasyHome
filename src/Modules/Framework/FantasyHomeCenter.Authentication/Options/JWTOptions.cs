// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using FantasyHomeCenter.Authentication.Enums;
using System.Collections.Generic;

namespace FantasyHomeCenter.Authentication.Options
{
    /// <summary>
    /// JWT 配置信息
    /// </summary>
    public class JWTOptions : IConfigurableOptions
    {
        /// <summary>
        /// 设置字典
        /// </summary>
        public Dictionary<IdentityType, JWTSettingsOptions> Settings { get; set; }
    }
}

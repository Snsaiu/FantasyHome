// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Client.Base.Services;

namespace FantasyHomeCenter.Client.Core.Services
{
    /// <summary>
    /// 系统配置-暂时写死吧
    /// </summary>
    [ScopedService]
    public class SystemConfigService : ISystemConfigService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCopyright()
        {
            return GetSystemConfig().Copyright;
        }
        /// <summary>
        /// 获取系统配置
        /// 可放置到数据库中
        /// </summary>
        /// <returns></returns>
        public SystemConfig GetSystemConfig()
        {

            return new SystemConfig {
            
                Copyright= DateTime.Now.Year+ " Fantasy Home",
                SystemName= "Fantasy Home",
                SystemDescription= ""

            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSystemName()
        {
            return GetSystemConfig().SystemName;
        }
    }
}

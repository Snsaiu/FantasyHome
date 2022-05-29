// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace FantasyHomeCenter.FileStore.Core.LocalStore
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalFileStoreSettings : IConfigurableOptions
    {
        /// <summary>
        /// 存储的基础目录,在wwwroot下
        /// 为空时，默认是upload 路径
        /// </summary>
        public string BaseDirectory { get; set; }
    }
}

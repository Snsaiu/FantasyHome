// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using AntDesign;

namespace FantasyHomeCenter.Client.Base
{
    /// <summary>
    /// Client导航标签控制器
    /// </summary>
    public class ClientNavTabControl
    {
        private static ReuseTabs _reuseTabs;

        public static void SetReuseTabs(ReuseTabs reuseTabs) 
        {
            _reuseTabs = reuseTabs;
            
        }

        /// <summary>
        /// 移除多标签导航
        /// </summary>
        public static void RemoveNavTabPageWithRegex(string pattern)
        {
            if (_reuseTabs != null)
            {
                _reuseTabs.RouteView.RemovePageWithRegex(pattern);
            }
        }

        /// <summary>
        /// 移除多标签导航
        /// </summary>
        public static void RemoveAllNavTabPage()
        {
            if (_reuseTabs != null)
            {
                _reuseTabs.RouteView.RemovePageWithRegex(".*");
            }
        }
    }
}

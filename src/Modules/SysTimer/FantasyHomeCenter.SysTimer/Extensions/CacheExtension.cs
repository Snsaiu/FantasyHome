// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.SysTimer
{
    public static class CacheExtension
    {
        public static bool Exists<T>(this ICache cache, string key)
        {
            if (cache == null)
                throw new ArgumentNullException();

            var data =  cache.Get<T>(key);

            return data == null?false:true;
        }
    }
}

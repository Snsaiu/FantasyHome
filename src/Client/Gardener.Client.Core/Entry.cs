﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Core
{
    /// <summary>
    /// 入口
    /// </summary>
    public static class Entry
    {
        static Entry()
        {
            Base.Entry.Add(typeof(Entry).Assembly);
        }
    }
}

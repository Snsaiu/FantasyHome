﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using System;
using System.Collections.Generic;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 客户端菜单缓存
    /// </summary>
    public static class ClientMenuCache
    {
        /// <summary>
        /// 以path为key的字典
        /// </summary>
        private static Dictionary<string, MenuDataItem> pathMap = new Dictionary<string, MenuDataItem>();

        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <param name="menu"></param>
        public static void Add(MenuDataItem menu)
        {
            if (string.IsNullOrEmpty(menu.Path))
            {
                return;
            }
            UriBuilder uriBuilder = new UriBuilder($"http://www.gardener.com{menu.Path}");
            if (!pathMap.ContainsKey(uriBuilder.Path))
            {
                pathMap.Add(uriBuilder.Path, menu);
            }
        }

        /// <summary>
        /// 根据path获取菜单信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MenuDataItem Get(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            if (pathMap.ContainsKey(path))
            {
                return pathMap[path];
            }
            return null;
        }
    }
}

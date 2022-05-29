﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FantasyHomeCenter.Base
{
    /// <summary>
    /// 查询表达式服务
    /// </summary>
    public class DynamicFilterService : IDynamicFilterService, ISingleton
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 初始化一个<see cref="DynamicFilterService"/>类型的新实例
        /// </summary>
        public DynamicFilterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Implementation of IFilterService

        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="groups">查询条件组，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        public virtual Expression<Func<T, bool>> GetExpression<T>(List<FilterGroup> groups)
        {
            return FilterHelper.GetExpression<T>(groups);
        }
        #endregion
    }
}

﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.UserCenter.Services
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public interface IResourceService : IServiceBase<ResourceDto, Guid>
    {
        /// <summary>
        /// 获取所有子资源
        /// </summary>
        /// <param name="id">父id</param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetChildren(Guid id);

        /// <summary>
        /// 返回根节点
        /// </summary>
        /// <returns></returns>
        Task<List<ResourceDto>> GetRoot();

        /// <summary>
        /// 查询所有资源 按树形结构返回
        /// </summary>
        /// <param name="rootKey"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetTree(string rootKey = null);
        
        /// <summary>
        /// 获取资源的种子数据
        /// </summary>
        /// <returns></returns>
        Task<String> GetSeedData();

        /// <summary>
        /// 根据资源id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<FunctionDto>> GetFunctions(Guid id);

    }
}
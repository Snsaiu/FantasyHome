﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    public interface IRoleService : IApplicationServiceBase<RoleDto,int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> DeleteResource( int roleId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        Task<bool> Resource(int roleId, Guid[] resourceIds);
        /// <summary>
        /// 获取有效的角色
        /// </summary>
        /// <returns></returns>
        Task<List<RoleDto>> GetEffective();
        /// <summary>
        /// 获取角色所有资源
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetResource(int roleId);

        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <remarks>
        /// 获取种子数据
        /// </remarks>
        /// <returns></returns>
        Task<string> GetRoleResourceSeedData();
    }
}
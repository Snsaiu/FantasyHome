﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Authorization.Services
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
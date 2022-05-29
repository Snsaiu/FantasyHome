﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;

namespace FantasyHomeCenter.UserCenter.Client.Services
{
    [ScopedService]
    public class RoleService : ClientServiceBase<RoleDto>,IRoleService
    {
        public RoleService(IApiCaller apiCaller):base(apiCaller, "role")
        {
        }

        public async Task<bool> DeleteResource(int roleId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{roleId}/resource");
        }

        public async Task<List<ResourceDto>> GetResource(int roleId)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/{roleId}/resource");
        }

        public async Task<string> GetRoleResourceSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/role-resource-seed-data");
        }

        public async Task<bool> Resource(int roleId, Guid[] resourceIds)
        {
            return await apiCaller.PostAsync<Guid[], bool>($"{controller}/{roleId}/resource", resourceIds);
        }
        public async Task<PagedList<RoleDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>() 
            {
                {"name",name }
            };
            return  await apiCaller.GetAsync<PagedList<RoleDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

    }
}

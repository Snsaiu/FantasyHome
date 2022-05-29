// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Common;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;

namespace FantasyHomeCenter.UserCenter.Client.Services
{
    [ScopedService]
    public class UserService : ClientServiceBase<UserDto>,IUserService
    {
        public UserService(IApiCaller apiCaller):base(apiCaller, "user")
        {
        }

        public Task<List<ResourceDto>> GetResources(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleDto>> GetRoles(int userId)
        {
            return await apiCaller.GetAsync<List<RoleDto>>($"{controller}/{userId}/roles");
        }
       
        public async Task<PagedList<UserDto>> Search(int[] deptIds, int pageIndex = 1, int pageSize = 10)
        {
            List<KeyValuePair<string, object>> pramas = deptIds.ConvertToQueryParameters("deptIds");

            return await apiCaller.GetAsync<PagedList<UserDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

        public async Task<bool> Role(int userId, int[] roleIds)
        {
            return await apiCaller.PostAsync<int[],bool>($"{controller}/{userId}/role", roleIds);
        }

        public async Task<bool> UpdateAvatar(UserUpdateAvatarInput input)
        {
            return await apiCaller.PutAsync<UserUpdateAvatarInput, bool>($"{controller}/avatar", input);
        }
    }
}

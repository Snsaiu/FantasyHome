﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.UserCenter.Services;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Enums;
using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.Client.Base;

namespace FantasyHomeCenter.UserCenter.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class AccountService : IAccountService
    {
        private readonly static string controller = "account";
        private IApiCaller apiCaller;

        public AccountService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<UserDto> GetCurrentUser()
        {
            return await apiCaller.GetAsync<UserDto>($"{controller}/current-user");
        }

        public async Task<List<ResourceDto>> GetCurrentUserMenus(string rootKey = null)
        {
            IDictionary<string, object> queryString = new Dictionary<string, object>();
            queryString.Add(nameof(rootKey), rootKey);
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-menus", queryString);
        }

        public async Task<List<string>> GetCurrentUserResourceKeys(params ResourceType[] resourceTypes)
        {
            List<KeyValuePair<string, object>> paras = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object>("resourceTypes", resourceTypes[i]));
            }
            return await apiCaller.GetAsync<List<string>>($"{controller}/current-user-resource-keys", paras);
        }

        public async Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType [] resourceTypes)
        {
            List<KeyValuePair<string, object>> paras = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object> ("resourceTypes" ,resourceTypes[i]));
            }
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-resources", paras);
        }

        public async Task<List<RoleDto>> GetCurrentUserRoles()
        {
            return await apiCaller.GetAsync<List<RoleDto>>($"{controller}/current-user-roles");
        }

        public async Task<TokenOutput> Login(LoginInput input)
        {
            var result = await apiCaller.PostAsync<LoginInput, TokenOutput>($"{controller}/login", input);
            return result;
        }

        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return await apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{controller}/refresh-token", input);
        }

        public async Task<bool> RemoveCurrentUserRefreshToken()
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/current-user-refresh-token");
        }
    }
}

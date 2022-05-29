// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;

namespace FantasyHomeCenter.UserCenter.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class ResourceFunctionService : IResourceFunctionService
    {
        private readonly static string controller = "resource-function";
        private readonly IApiCaller apiCaller;
        public ResourceFunctionService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<bool> Add(List<ResourceFunctionDto> resourceFunctionDtos)
        {
            return await apiCaller.PostAsync<List<ResourceFunctionDto>,bool>($"{controller}", resourceFunctionDtos);
        }

        public async Task<bool> Delete(Guid resourceId, Guid functionId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{resourceId}/{functionId}");
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }
    }
}

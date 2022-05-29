﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;

namespace Gardener.UserCenter.Client.Services
{
    [ScopedService]
    public class ResourceService : ClientServiceBase<ResourceDto,Guid>,IResourceService
    {
        public ResourceService(IApiCaller apiCaller):base(apiCaller, "resource")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetChildren(Guid id)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/{id}/children");
        }

        public async Task<List<FunctionDto>> GetFunctions(Guid id)
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{controller}/{id}/functions");
        }

        public async Task<List<ResourceDto>> GetRoot()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/root");
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }

        public async Task<List<ResourceDto>> GetTree(string rootKey=null)
        {
            IDictionary<string, object> queryString=new Dictionary<string, object>();
            queryString.Add(nameof(rootKey), rootKey);
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/tree", queryString);
        }
    }
}

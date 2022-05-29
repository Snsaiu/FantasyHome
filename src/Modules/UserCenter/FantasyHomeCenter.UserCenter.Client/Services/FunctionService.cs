// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Enums;
using System;
using System.Threading.Tasks;
using System.Web;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;
using FantasyHomeCenter.Client.Base;

namespace FantasyHomeCenter.UserCenter.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class FunctionService : ClientServiceBase<FunctionDto, Guid>, IFunctionService
    {
        public FunctionService(IApiCaller apiCaller) : base(apiCaller, "function")
        {
        }

        public async Task<bool> EnableAudit(Guid id, bool enableAudit=true)
        {
            return await apiCaller.PutAsync<bool, bool>($"{controller}/{id}/enable-audit/{enableAudit}");
        }

        public async Task<bool> Exists(HttpMethod method, string path)
        {
            path=HttpUtility.UrlEncode(path);
            return await apiCaller.GetAsync<bool>($"{controller}/exists/{method}/{path}");
        }

        public async Task<FunctionDto> GetByKey(string key)
        {
            return await apiCaller.GetAsync<FunctionDto>($"{controller}/by-key/{key}");
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }
    }
}

// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;

namespace Gardener.UserCenter.Client.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    [ScopedService]
    public class DeptService : ClientServiceBase<DeptDto>, IDeptService
    {
        public DeptService(IApiCaller apiCaller) : base(apiCaller, "dept")
        {
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }

        public async Task<List<DeptDto>> GetTree()
        {
            return await apiCaller.GetAsync<List<DeptDto>>($"{controller}/tree");
        }
    }
}

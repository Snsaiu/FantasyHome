// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Base;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.UserCenter.Client.Services
{
    [ScopedService]
    public class PositionService : ClientServiceBase<PositionDto, int>, IPositionService
    {
        public PositionService(IApiCaller apiCaller) : base(apiCaller, "position")
        {
        }
        
        public async Task<PagedList<PositionDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>()
            {
                {"name",name }
            };
            return await apiCaller.GetAsync<PagedList<PositionDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }
    }
}

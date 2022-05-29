// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Swagger.Dtos;
using FantasyHomeCenter.Swagger.Services;

namespace FantasyHomeCenter.Swagger.Client.Services
{
    [ScopedService]
    public class SwaggerService : ISwaggerService
    {
        private readonly static string controller = "swagger";
        private readonly IApiCaller apiCaller;

        public SwaggerService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<SwaggerModel> Analysis(string url)
        {
            url = HttpUtility.UrlEncode(url);
            return await apiCaller.GetAsync<SwaggerModel>($"{controller}/analysis/{url}");
        }

        public async Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            return await apiCaller.GetAsync<List<SwaggerSpecificationOpenApiInfoDto>>($"{controller}/api-group");
        }

        public async Task<List<ApiEndpoint>> GetFunctionsFromJson(string url)
        {
            url = HttpUtility.UrlEncode(url);
            return await apiCaller.GetAsync<List<ApiEndpoint>>($"{controller}/functions-from-json/{url}");
        }
    }
}

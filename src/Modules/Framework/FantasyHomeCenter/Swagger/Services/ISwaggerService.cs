// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.Swagger.Dtos;

namespace FantasyHomeCenter.Swagger.Services
{
    /// <summary>
    /// swagger 服务
    /// </summary>
    public interface ISwaggerService
    {
        /// <summary>
        /// 解析open api json
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<SwaggerModel> Analysis(string url);

        /// <summary>
        /// 获取 swagger 配置
        /// </summary>
        /// <returns></returns>
        Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup();

        /// <summary>
        /// 从json中获取function
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<List<ApiEndpoint>> GetFunctionsFromJson(string url);
    }
}

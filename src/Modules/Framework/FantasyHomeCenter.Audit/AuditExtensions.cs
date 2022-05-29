// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Audit.Core;
using FantasyHomeCenter.EntityFramwork.Audit.Core;
using FantasyHomeCenter.EntityFramwork.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 审计
    /// </summary>
    public static class AuditExtensions
    {
        /// <summary>
        /// 添加审计服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAudit(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                //审计过滤器
                options.Filters.Add<AuditActionFilter>();
            });
            //数据管理
            services.AddScoped<IOrmAuditService, EntityFramworkAuditService<FantasyHomeCenterAuditDbContextLocator>>();
            return services;
        }
    }
}

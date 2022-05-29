// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Audit.Dtos;
using FantasyHomeCenter.Audit.Services;
using FantasyHomeCenter.Client.Base;
using System;

namespace Gardener.Audit.Client.Services
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ScopedService]
    public class AuditEntityService : ClientServiceBase<AuditEntityDto, Guid>, IAuditEntityService
    {
        public AuditEntityService(IApiCaller apiCaller) : base(apiCaller, "audit-entity")
        {
        }
    }
}

// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Audit.Dtos;
using FantasyHomeCenter.Audit.Services;
using FantasyHomeCenter.Client.Base;

namespace Gardener.Audit.Client.Services
{
    [ScopedService]
    public class AuditOperationService : ClientServiceBase<AuditOperationDto, Guid>, IAuditOperationService
    {
        public AuditOperationService(IApiCaller apiCaller) : base(apiCaller, "audit-operation")
        {
        }

        public async Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId)
        {
            return await apiCaller.GetAsync<List<AuditEntityDto>>($"{controller}/{operationId}/audit-entity");
        }
    }
}

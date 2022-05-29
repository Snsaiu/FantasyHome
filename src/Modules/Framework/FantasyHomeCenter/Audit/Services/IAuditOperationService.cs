// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Audit.Dtos;
using FantasyHomeCenter.Base;

namespace FantasyHomeCenter.Audit.Services
{
    /// <summary>
    /// 审计操作服务
    /// </summary>
    public interface IAuditOperationService : IServiceBase<AuditOperationDto, Guid>
    {
        /// <summary>
        /// 根据操作审计ID获取数据审计数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId);
    }
}

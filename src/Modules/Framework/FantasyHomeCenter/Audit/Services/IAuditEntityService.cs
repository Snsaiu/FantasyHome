// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using FantasyHomeCenter.Audit.Dtos;
using FantasyHomeCenter.Base;

namespace FantasyHomeCenter.Audit.Services
{
    /// <summary>
    /// 审计数据服务接口
    /// </summary>
    public interface IAuditEntityService : IServiceBase<AuditEntityDto, Guid>
    {
        
    }
}

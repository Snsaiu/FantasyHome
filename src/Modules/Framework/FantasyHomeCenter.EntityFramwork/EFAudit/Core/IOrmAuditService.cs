// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.EntityFramwork.Audit.Domains;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.EntityFramwork.Audit.Core
{
    /// <summary>
    /// Orm审计服务接口
    /// </summary>
    public interface IOrmAuditService
    {
        /// <summary>
        /// 数据保存前
        /// </summary>
        /// <param name="entitys"></param>
        public void SavingChangesEvent(IEnumerable<EntityEntry> entitys);
        /// <summary>
        /// 数据保存后
        /// </summary>
        public Task SavedChangesEvent();

        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditOperation(AuditOperation auditOperation);

    }
}

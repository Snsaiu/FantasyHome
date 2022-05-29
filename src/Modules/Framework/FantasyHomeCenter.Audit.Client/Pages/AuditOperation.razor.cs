// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Audit.Dtos;
using FantasyHomeCenter.Audit.Services;
using FantasyHomeCenter.Client.Base.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Audit.Client.Pages
{
    public partial class AuditOperation : TableBase<AuditOperationDto, Guid>
    {
        [Inject]
        public IAuditOperationService auditOperationService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(Guid id)
        {
            List<AuditEntityDto>  auditEntityDtos= await auditOperationService.GetAuditEntity(id);

            await drawerService.CreateDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(auditEntityDtos, title: localizer["详情"], width: 960, placement: "left");
        }
    }
}

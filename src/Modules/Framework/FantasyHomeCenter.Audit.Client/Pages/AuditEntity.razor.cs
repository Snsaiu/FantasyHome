﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Audit.Dtos;
using FantasyHomeCenter.Client.Base.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Audit.Client.Pages
{
    public partial class AuditEntity : TableBase<AuditEntityDto, Guid>
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(AuditEntityDto auditEntity)
        {
          await drawerService.CreateDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(new[] { auditEntity }, title: "字段变更详情", width: 960, placement:"left");
        }
    }
}

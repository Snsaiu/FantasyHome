﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Audit.Dtos;
using Gardener.Audit.Services;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.EntityFramwork.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Audit.Client.Pages
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

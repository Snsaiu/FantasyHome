﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using System;
using System.Threading.Tasks;

namespace FantasyHomeCenter.UserCenter.Client.Pages.ClientView
{
    public partial class ClientEdit : EditDrawerBase<ClientDto, Guid>
    {

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (this.Options.Type.Equals(DrawerInputType.Add))
            { 
                 _editModel.Id = Guid.NewGuid();
                 _editModel.SecretKey = Guid.NewGuid().ToString();
            }
        }
    }
}

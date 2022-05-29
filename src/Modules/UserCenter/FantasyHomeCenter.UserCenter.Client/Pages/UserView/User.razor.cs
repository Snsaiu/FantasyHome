﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Common;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;
using Gardener.Client.Base.Components;
using Gardener.Base;
using Microsoft.AspNetCore.Components.Web;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class User : TableBase<UserDto, int, UserEdit>
    {
        private Tree<DeptDto> _deptTree;
        private List<DeptDto> depts;
        private bool _deptTreeIsLoading = false;
        [Inject]
        IDeptService deptService { get; set; }
        int _currentDeptId = 0;
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await ReLoadDepts(null);
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 重载部门信息
        /// </summary>
        /// <returns></returns>
        private async Task ReLoadDepts(MouseEventArgs eventArgs)
        {
            _deptTreeIsLoading = true;
            depts = await deptService.GetTree();
            _deptTreeIsLoading = false;
        }
        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        private async Task SelectedKeyChanged(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _currentDeptId = 0;
            }
            else 
            {
                int newId = int.Parse(key);
                _currentDeptId = newId;
            }
            await ReLoadTable();
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected override void ConfigurationPageRequest(PageRequest pageRequest)
        {
            if (_currentDeptId>0)
            {
                var node = TreeTools.QueryNode(depts, d => d.Id.Equals(_currentDeptId), d => d.Children);
                List<int> ids = TreeTools.GetAllChildrenNodes(node, d => d.Id, d => d.Children);
                if (ids != null)
                {
                    pageRequest.FilterGroups.Add(new FilterGroup().AddRule(new FilterRule(nameof(UserDto.DeptId), ids, FilterOperate.In)));
                }
            }
        }

        /// <summary>
        /// 点击分配角色
        /// </summary>
        /// <param name="userId"></param>
        private async Task OnEditUserRoleClick(int userId)
        {
            var result = await drawerService.CreateDialogAsync<UserRoleEdit, int, bool>(userId, true, title: localizer["设置角色"], width: 500);

            if (result)
            {
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            int avatarDrawerWidth = 300;
            await drawerService.CreateDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(new UserUploadAvatarParams { User = user, SaveDb = true }, true, title: "上传头像", width: avatarDrawerWidth, placement: "left");
        }

    }
}

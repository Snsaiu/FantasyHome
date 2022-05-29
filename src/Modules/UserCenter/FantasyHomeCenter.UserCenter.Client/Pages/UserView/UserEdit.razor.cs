// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.UserCenter.Client.Pages.UserView
{
    public partial class UserEdit: EditDrawerBase<UserDto, int>
    {
        private List<PositionDto> positions = new List<PositionDto>();
        [Inject]
        IDeptService deptService { get; set; }
        [Inject]
        IPositionService positionService { get; set; }
        //部门树
        List<DeptDto> deptDatas=new List<DeptDto>();
        /// <summary>
        /// 部门编号
        /// </summary>
        protected string deptId 
        {
            get {
                return _editModel.DeptId?.ToString();
            }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.DeptId = int.Parse(value);
                }
                else 
                {
                    _editModel.DeptId = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            positions = await positionService.GetAllUsable();
            //部门
            deptDatas = await deptService.GetTree();
            _isLoading = false;
            await base.OnInitializedAsync();
            _editModel.Password = null;
            _editModel.UserExtension = _editModel.UserExtension ?? new UserExtensionDto() { UserId=_editModel.Id};
        }
       
        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            int avatarDrawerWidth = 300;
            //this.DrawerRef.Options.Width += avatarDrawerWidth;
            await drawerService.CreateDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(new UserUploadAvatarParams { User =user }, true, title: "上传头像", width: avatarDrawerWidth, placement: "right");
            //this.DrawerRef.Options.Width -= avatarDrawerWidth;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void OnSelectedItemChangedHandler(PositionDto value)
        {
            
        }
    }
}

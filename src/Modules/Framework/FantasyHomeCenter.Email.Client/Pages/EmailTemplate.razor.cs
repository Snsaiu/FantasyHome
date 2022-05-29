// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Client.Base.Components;
using FantasyHomeCenter.Client.Base.Model;
using FantasyHomeCenter.Email.Dtos;
using System;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Email.Client.Pages
{
    public partial class EmailTemplate : TableBase<EmailTemplateDto, Guid, EmailTemplateEdit>
    {
        protected override DrawerSettings GetDrawerSettings()
        {
            return new DrawerSettings { Width = 1000 };
        }

        /// <summary>
        /// 点击发送按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickSend(Guid id)
        {
            DrawerSettings drawerSettings = GetDrawerSettings();
            DrawerInput<Guid> input = DrawerInput<Guid>.IsSelect(id);
            var result = await drawerService.CreateDialogAsync<EmailTemplateTest, DrawerInput<Guid>, DrawerOutput<Guid>>(
                input,
                closable: drawerSettings.Closable,
                title: localizer["发送"],
                width: drawerSettings.Width,
                placement: drawerSettings.Placement.ToString().ToLower());
        }
    }
}

// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using Microsoft.AspNetCore.Components;

namespace FantasyHomeCenter.Client.Entry
{
    public partial class App
    {
        [Inject]
        private ClientModuleContext moduleContext { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        private void GoHome() 
        {
            Navigation.NavigateTo("/");
        }
    }
}

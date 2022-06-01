
using AntDesign;
using FantasyHomeCenter.Application.UserCenter;
using FantasyHomeCenter.Application.UserCenter.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Login
{

    private LoginUserInput model = new LoginUserInput();

    private bool isRemember = true;

    private bool loading = false;

    
    [Inject]
    private MessageService MessageService { get; set; }
    
    [Inject]
    private IUserService userService { get; set; }
    
    [Inject]
    private ProtectedLocalStorage LocalStorageService { get; set; }
    
    [Inject]
    private NavigationManager navigationManager { get; set; }
    
    [Inject]
    private AuthenticationStateProvider authenticationStateProvider { get; set; }
    
    private async Task OnFinish(EditContext editContext)
    {
        this.loading = true;
        await this.login();
        this.loading = false;
    }

    private void OnFinishFailed(EditContext editContext)
    {
        
    }

    /// <summary>
    /// 用户登陆
    /// </summary>
    /// <returns></returns>
    private async Task login()
    {
        var res=  this.userService.Author(this.model);
       if (res.Token == "")
       {
           this.MessageService.Error("登陆失败！");
           return;
           
       }
       else
       { 
           if(this.isRemember)
                await  this.LocalStorageService.SetAsync("token", res.Token);
           await this.authenticationStateProvider.GetAuthenticationStateAsync();
            this.navigationManager.NavigateTo("/",replace:true);
       }
    }
}
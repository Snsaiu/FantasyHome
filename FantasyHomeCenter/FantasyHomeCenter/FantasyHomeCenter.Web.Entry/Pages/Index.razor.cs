
using FantasyHomeCenter.Application;
using Furion.RemoteRequest.Extensions;
using Microsoft.AspNetCore.Components;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Index
{

    [Inject]
    private ISystemService SystemService { get; set; }


    protected override void OnInitialized()
    {
        base.OnInitialized();
      var data =this.SystemService.GetDescription();
    }

    protected override async Task OnInitializedAsync()
    {
     //   await base.OnInitializedAsync();
    
    }
}
namespace FantasyHome.UI.ViewModels;

public partial class ViewModelBase:ObservableObject
{

    [ObservableProperty]
    private bool isBusy=false;

    protected bool BusyState = false;

    [ICommand]
    private async Task Load()
    {
     
        if(BusyState)
            return;
        this.BusyState = true;
        this.IsBusy = true;
        // this.Loading();
        // this.BusyState = false;
        // this.IsBusy = false;

        Task.Run(this.Loading).ConfigureAwait(false).GetAwaiter().OnCompleted(() =>
        {
            this.BusyState = false;
            this.IsBusy = false;
            
        });
      
        
    
    }

    protected virtual async Task Loading()
    {

    }

}
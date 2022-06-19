namespace FantasyHome.UI.ViewModels;

public partial class ViewModelBase:ObservableObject
{

    [ObservableProperty]
    private bool isBusy=false;

    protected bool BusyState = false;

    [ICommand]
    private void Load()
    {
     
        if(BusyState)
            return;
        this.BusyState = true;
        Task.Run(() => this.Loading()).GetAwaiter().OnCompleted(() =>
        {
            this.BusyState = false;
            this.IsBusy = false;
        });
    }

    protected virtual void Loading()
    {

    }

}
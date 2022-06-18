using FantasyHome.Application;
using FantasyHome.Application.Dto;

namespace FantasyHome.UI;

[ObservableObject]
public partial class AppShellModel
{
    private readonly IRoomApplication roomApplication;

    public AppShellModel(IRoomApplication roomApplication)
    {
        this.roomApplication = roomApplication;
        this.getRooms();
    }

    [ObservableProperty]
    private ObservableCollection<FlyoutItem> flymenuItems=new ObservableCollection<FlyoutItem>();

    /// <summary>
    /// 获得房间集合
    /// </summary>
    /// <returns></returns>
    private async Task getRooms()
    {
        FlyoutItem item = new FlyoutItem();
        item.Title = "主页";
        item.Route = "主页";
        this.FlymenuItems.Add(item);

        return;
        var roomsRes= await this.roomApplication.GetRooms();
        if (roomsRes.Succeeded)
        {

            
        

        }
        else
        {
            await Shell.Current.DisplayAlert("警告", "未发现房间","确定");
        }
    }
    
}
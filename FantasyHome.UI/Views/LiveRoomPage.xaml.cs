using FantasyHome.UI.ViewModels;

namespace FantasyHome.UI.Views;

public partial class LiveRoomPage : ContentPage
{
	public LiveRoomPage(LiveRoomPageModel roomPageModel)
	{
		InitializeComponent();
		this.BindingContext = roomPageModel;
		this.aircontrol.SendActionEvent += (model =>
		{
			roomPageModel.SetAirState(model);
			
		});
	}
}

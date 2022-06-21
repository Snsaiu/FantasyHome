using FantasyHome.UI.ViewModels;

namespace FantasyHome.UI.Views;

public partial class MasterBedRoomPage : ContentPage
{
	public MasterBedRoomPage(MasterBedRoomPageModel pageModel)
	{
		InitializeComponent();
		this.BindingContext = pageModel;
	}
}

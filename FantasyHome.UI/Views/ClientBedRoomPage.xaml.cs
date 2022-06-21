using FantasyHome.UI.ViewModels;

namespace FantasyHome.UI.Views;

public partial class ClientBedRoomPage : ContentPage
{
	public ClientBedRoomPage(ClientBedRoomPageModel pageModel)
	{
		InitializeComponent();
		this.BindingContext = pageModel;
	}
}

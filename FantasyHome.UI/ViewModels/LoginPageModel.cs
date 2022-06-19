
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FantasyHome.UI.ViewModels
{
	public partial class LoginPageModel:ViewModelBase
	{
		public LoginPageModel()
		{

		}

		/// <summary>
		/// 登陆命令
		/// </summary>
		/// <returns></returns>
		[ICommand]
		private async Task Login()
		{
			this.IsBusy = true;

			await Task.Delay(500);
			this.IsBusy = false;
			await Shell.Current.GoToAsync($"//{nameof(Views.MainPage)}");
			
		}

	}
}


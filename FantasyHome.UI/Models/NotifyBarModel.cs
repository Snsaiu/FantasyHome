using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FantasyHome.UI.Models
{
	public class NotifyBarModel:ObservableObject
	{
		public NotifyBarModel()
		{

		}

		[ObservableProperty]
		private string id;

		[ObservableProperty]
		private string title;




	}
}


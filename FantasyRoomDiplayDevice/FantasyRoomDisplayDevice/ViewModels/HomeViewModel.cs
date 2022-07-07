using Microsoft.Toolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using FantasyRoomDisplayDevice.Views;
using Microsoft.Toolkit.Mvvm.Input;
using Prism.Regions;

namespace FantasyRoomDisplayDevice.ViewModels
{
    [ObservableObject]
    public partial class HomeViewModel 
    {
        private readonly IRegionManager regionManager;

        public HomeViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        [ICommand]
        private void Loaded()
        {
            this.regionManager.RequestNavigate("ItemRegion", nameof(HomeComponent));
        }

        [ICommand]
        private void OpenComponentPage(object obj)
        {
            string pageName = obj.ToString();
            switch (pageName)
            {
                case "主页":
                    this.regionManager.RequestNavigate("ItemRegion",nameof(HomeComponentViewModel));
                    break;
                default:
                    break;
            }
        }

    }
}

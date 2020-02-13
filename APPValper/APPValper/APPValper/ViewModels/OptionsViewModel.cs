using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using APPValper.Models;
using APPValper.Views;
using APPValper.Services;
using System.Collections.Generic;

namespace APPValper.ViewModels
{
    class OptionsViewModel : BaseViewModel
    {
        public Command HomeCommand { get; set; }
        INavigation Navigation;
        public OptionsViewModel(INavigation Nav)
        {
            Navigation = Nav;
            HomeCommand = new Command(async () => await Home(), () => !IsBusy);
        }

        private async Task Home()
        {
            await Navigation.PushAsync(new ItemsPage());
        }
    }
}

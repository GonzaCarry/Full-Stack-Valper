using APPValper.Models;
using APPValper.Services;
using APPValper.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace APPValper.ViewModels
{
    public class LoginViewModel : Connection
    {
        LoginActiveDirectory service = new LoginActiveDirectory();
        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        INavigation Navigation;
        public LoginViewModel(INavigation Nav)
        {
            Navigation = Nav;
            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            RegisterCommand = new Command(async () => await Register(), () => !IsBusy);
        }

        private async Task Login()
        {
            IsBusy = true;
            Console.WriteLine(Username);
            Console.WriteLine(Password);
            service.Login(Username, Password);
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task Register()
        {
            await Navigation.PushAsync(new Register());
        }
    }
}

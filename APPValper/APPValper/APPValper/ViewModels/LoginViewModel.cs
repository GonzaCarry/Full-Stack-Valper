using APPValper.Models;
using APPValper.Services;
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
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            //RegisterCommand = new Command(async () => await Register(), () => !IsBusy);
        }

        private async Task Login()
        {
            IsBusy = true;
            service.Login(Username, Password);
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}

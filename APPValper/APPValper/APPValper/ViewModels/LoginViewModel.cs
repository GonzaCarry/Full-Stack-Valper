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
        public string Email { get; set; }
        public string Password { get; set; }
        INavigation Navigation;
        private int logging;
        private bool logged = false;
        private User userAux;
        public LoginViewModel(INavigation Nav)
        {
            Navigation = Nav;
            //LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            RegisterCommand = new Command(async () => await Register(), () => !IsBusy);
        }

        //private async Task Login()
        //{
        //    IsBusy = true;
        //    Console.WriteLine(Username);
        //    Console.WriteLine(Password);
        //    service.Login(Username, Password);
        //    await Task.Delay(2000);
        //    IsBusy = false;
        //}

        private async Task Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El email no puede ser nulo", "Aceptar");
            }
            else
            {
                if (string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "La contraseña no puede ser nula", "Aceptar");
                }
                else
                {
                    foreach (User user in User.Users)
                    {
                        logging = 0;
                        if (Email.Equals(user.Email))
                        {
                            logging++;
                        }
                        if (Password.Equals(user.Email))
                        {
                            logging++;
                        }
                        if (logging == 2)
                        {
                            logged = true;
                            userAux = user;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Usuario incorrecto", "Aceptar");
                        }
                    }
                    if (logged)
                    {
                        User.Users[0] = userAux;
                        User.Users[0].Logged = true;
                        await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha conectado correctamente", "Aceptar");
                        await Navigation.PopToRootAsync();
                    }
                }
            }
        }

        private async Task Register()
        {
            await Navigation.PushAsync(new Register());
        }
    }
}

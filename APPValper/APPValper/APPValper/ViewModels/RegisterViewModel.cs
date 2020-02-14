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
    class RegisterViewModel : Connection
    {
        public Command RegisterCommand { get; set; }
        public Command AdminCommand { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        INavigation Navigation;
        private bool admin = false;
        private bool registered = false;
        public RegisterViewModel(INavigation Nav)
        {
            Navigation = Nav;
            RegisterCommand = new Command(async () => await Register(), () => !IsBusy);
            AdminCommand = new Command(async () => await Admin(), () => !IsBusy);
        }

        private async Task Admin()
        {
            if (!admin)
            {
                await Application.Current.MainPage.DisplayAlert("Felicidades", "Serás administrador.", "Aceptar");
                admin = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Ya no serás administrador.", "Aceptar");
                admin = false;
            }
        }

        private async System.Threading.Tasks.Task Register()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El email no puede ser nulo", "Aceptar");
            }
            else
            {
                if (string.IsNullOrEmpty(Username))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El nombre no puede ser nulo", "Aceptar");
                }
                else
                {
                    if (string.IsNullOrEmpty(Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "La primera contraseña no puede ser nulo", "Aceptar");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Password2))
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "La segunda contraseña no puede ser nulo", "Aceptar");
                        }
                        else
                        {
                            if (!Password.Equals(Password2))
                            {
                                await Application.Current.MainPage.DisplayAlert("Error", "Ambas contraseñas deben ser iguales", "Aceptar");
                            }
                            else
                            {
                                foreach(User user in User.Users)
                                {
                                    if (Email.Equals(user.Email))
                                    {
                                        registered = true;
                                        await Application.Current.MainPage.DisplayAlert("Error", "Ese email ya ha sido registrado", "Aceptar");
                                    }
                                }
                                if (!registered)
                                {
                                    User.Users[0] = new User(Email, Username, Password, false, admin);
                                    await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha registrado satisfactoriamente", "Aceptar");
                                    await Navigation.PopToRootAsync();
                                }
                                registered = false;
                            }
                        }
                    }
                }
            }
        }
    }
}

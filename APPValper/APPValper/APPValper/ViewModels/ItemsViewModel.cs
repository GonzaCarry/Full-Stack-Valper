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
    public class ItemsViewModel : BaseViewModel
    {
        OptionsService service = new OptionsService();

        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command CRUDCommand { get; set; }
        public Command ChangeUserCommand { get; set; }
        public Command SaveCommand { get; set; }
        public Command ExitCommand { get; set; }
        //public Frame SpanishFrame { get; set; }
        //public Frame EnglishFrame { get; set; }

        public Command OptionsCommand { get; set; }
        public Command SpanishCommand { get; set; }
        public Command EnglishCommand { get; set; }
        INavigation Navigation;

        public Connection Con { get; set; }
        public string Url { get; set; }

        public ItemsViewModel(INavigation Nav)
        {
            Update();
            Navigation = Nav;
            EnglishCommand = new Command(async () => await English(), () => !IsBusy);
            SpanishCommand = new Command(async () => await Spanish(), () => !IsBusy);
            SaveCommand = new Command(async () => await Save(), () => !IsBusy);
            OptionsCommand = new Command(async () => await Options(), () => !IsBusy);
            CRUDCommand = new Command(async () => await CRUD(), () => !IsBusy);
            ChangeUserCommand = new Command(async () => await ChangeUser(), () => !IsBusy);
            ExitCommand = new Command(Exit);
            Title = "Home";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        private void Update()
        {
            Url = service.ConsultLocal().Url;
        }

        private async Task Save()
        {
            bool successful;
            IsBusy = true;
            Con = new Connection()
            {
                Url = Url
            };
            if (string.IsNullOrEmpty(Con.Url))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La dirección no puede ser nula", "Aceptar");
            }
            else
            {
                Console.WriteLine(Con.Url);
                successful = await service.SaveLocalAsync(Con);
                if (successful)
                {
                    await Application.Current.MainPage.DisplayAlert("Felicidades", "Se ha conectado satisfactoriamente a la url: " + Con.Url, "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido conectar a la url: " + Con.Url, "Aceptar");
                }
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task English()
        {
            await Application.Current.MainPage.DisplayAlert("Atención", "Se ha cambiado el idioma a inglés.", "Aceptar");
            //EnglishFrame.BackgroundColor = Color.Yellow;
            //SpanishFrame.BackgroundColor = Color.White;
            //await Navigation.PushAsync(new ItemsPage());
        }

        private async Task Spanish()
        {
            await Application.Current.MainPage.DisplayAlert("Atención", "Se ha cambiado el idioma a español.", "Aceptar");
            //EnglishFrame.BackgroundColor = Color.White;
            //SpanishFrame.BackgroundColor = Color.Yellow;
            //await Navigation.PushAsync(new ItemsPage());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CRUD()
        {
            if (User.Users[0].Logged)
            {
                await Navigation.PushAsync(new Functions());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe estar conectado para acceder", "Aceptar");
            }
            
        }

        private async Task Options()
        {
            await Navigation.PushAsync(new Options());
        }

        private async Task ChangeUser()
        {
            await Navigation.PushAsync(new Login());
        }

        private void Exit()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

    }
}
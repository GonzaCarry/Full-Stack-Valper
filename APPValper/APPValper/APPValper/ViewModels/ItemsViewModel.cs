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
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command CRUDCommand { get; set; }
        public Command ChangeUserCommand { get; set; }
        public Command ExitCommand { get; set; }
        INavigation Navigation;

        public ItemsViewModel(INavigation Nav)
        {
            Navigation = Nav;
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
            await Navigation.PushAsync(new Functions());
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
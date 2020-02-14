using APPValper.Models;
using APPValper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPValper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        private static List<HomeMenuItem> menuItems { get; set; }
        public static bool logged { get; set; } = false;
        public MenuPage()
        {
            InitializeComponent();
            User.Users.Add(new User("test", "test", "test", false, false));
            FillList();
            Update();
        }

        public static void FillList()
        {
            Console.WriteLine("pasaxd");

            menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Home, Title="Home" },
                    new HomeMenuItem {Id = MenuItemType.EditUser, Title="User configuration" },
                    new HomeMenuItem {Id = MenuItemType.Options, Title="Options" }
                };
            //if (User.Users[0].Logged)
            //{
            //    Console.WriteLine("nolog");
            //    menuItems = new List<HomeMenuItem>
            //    {
            //        new HomeMenuItem {Id = MenuItemType.Home, Title="Home" },
            //        new HomeMenuItem {Id = MenuItemType.Options, Title="Options" }
            //    };
            //}
            //else
            //{
            //    Console.WriteLine("logeado");
                
            //}
        }

        public void Update()
        {
            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                if (e.SelectedItemIndex == 1)
                {
                    if (User.Users[0].Logged)
                    {
                        var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                        await RootPage.NavigateFromMenu(id);
                    }
                    else
                    {
                        ListViewMenu.SelectedItem = menuItems[0];
                        await Application.Current.MainPage.DisplayAlert("Error", "Debe estar conectado", "Aceptar");
                    }
                }
                else
                {
                    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                    await RootPage.NavigateFromMenu(id);
                }
            };
        }
    }
}
using APPValper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPValper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();
            ItemsViewModel Context = new ItemsViewModel(Navigation);
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = Context;
        }
    }
}
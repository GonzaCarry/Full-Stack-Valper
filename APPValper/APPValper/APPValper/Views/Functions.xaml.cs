using APPValper.ViewModels;
using APPValper.Models;
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
    public partial class Functions : ContentPage
    {
        FunctionsViewModel Context = new FunctionsViewModel();
        public Functions()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = Context;
            LvFunctions.ItemSelected += LvFunctions_ItemSelected;
        }

        private void LvFunctions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Function model = (Function)e.SelectedItem;
                if (Context.GoBool)
                {
                    ((ListView)sender).SelectedItem = null;
                    //Navigation.PushAsync(new DetallePage(model));
                }
                Context.Server = model.Server;
                Context.Action = model.Action;
                Context.Description = model.Description;
                Context.Id = model.Id;
            }
        }
    }
}
﻿using APPValper.ViewModels;
using APPValper.Models;
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
    public partial class Functions : TabbedPage
    {
        FunctionsViewModel Context = new FunctionsViewModel();
        public Functions()
        {
            Console.WriteLine("pasa");
            InitializeComponent();

            BindingContext = Context;
            //LvFunctions.ItemSelected += LvFunctions_ItemSelected;
        }

        //private void LvFunctions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem != null)
        //    {
        //        Function model = (Function)e.SelectedItem;
        //        if (Context.GoBool)
        //        {
        //            ((ListView)sender).SelectedItem = null;
        //            //Navigation.PushAsync(new DetallePage(model));
        //        }
        //        Context.Server = model.Server;
        //        Context.Action = model.Action;
        //        Context.Description = model.Description;
        //        Context.Id = model.Id;
        //    }
        //}
    }
}
using APPValper.Models;
using APPValper.Resources;
using APPValper.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace APPValper.ViewModels
{
    class FunctionsViewModel : Function
    {
        private Task<ObservableCollection<Function>> FunctionsTask { get; set; }
        private ObservableCollection<Function> FunctionsAux { get; set; }
        public ObservableCollection<Function> Functions { get; set; }

        Function model;

        FunctionsService service = new FunctionsService();

        public FunctionsViewModel()
        {
            ListView();
            ListViewAsync();
            SaveCommand = new Command(async () => await Save(), () => !IsBusy);
            ModifyCommand = new Command(async () => await Modify(), () => !IsBusy);
            DeleteCommand = new Command(async () => await Delete(), () => !IsBusy);
            CleanCommand = new Command(Clean, () => !IsBusy);
            GoCommand = new Command(Go, () => !IsBusy);
            BackCommand = new Command(Back, () => !IsBusy);
        }

        private void ListView()
        {
            Functions = new ObservableCollection<Function>();
            FunctionsAux = service.ConsultLocal();
            for (int i = 0; i < FunctionsAux.Count; i++)
            {
                Functions.Add(FunctionsAux[i]);
            }
        }

        private async Task ListViewAsync()
        {
            FunctionsTask = service.Consult();
            FunctionsAux = await FunctionsTask;
            for (int i = 0; i < FunctionsAux.Count; i++)
            {
                Functions.Add(FunctionsAux[i]);
            }
        }

        public bool GoBool { get; set; }

        public Command SaveCommand { get; set; }

        public Command ModifyCommand { get; set; }

        public Command DeleteCommand { get; set; }

        public Command CleanCommand { get; set; }

        public Command GoCommand { get; set; }

        public Command BackCommand { get; set; }

        private async Task Save()
        {
            IsBusy = true;
            Guid idFunction = Guid.NewGuid();
            model = new Function()
            {
                Server = Server,
                Action = Action,
                Description = Description,
                Id = idFunction.ToString()
            };
            if (string.IsNullOrEmpty(model.Server))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El servidor no puede ser nulo", "Aceptar");
            }
            else
            {
                service.Save(model);
                service.SaveLocal(model);
                Functions.Add(model);
                Clean();
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task Modify()
        {
            IsBusy = true;
            model = new Function()
            {
                Server = Server,
                Action = Action,
                Description = Description,
                Id = Id
            };
            service.Modify(model);
            service.ModifyLocal(model);
            var item = Functions.FirstOrDefault(i => i.Id == model.Id);
            if (item != null)
            {
                item.Server = model.Server;
                item.Action = model.Action;
                item.Description = model.Description;
            }
            Clean();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task Delete()
        {
            IsBusy = true;
            model = new Function()
            {
                Server = Server,
                Action = Action,
                Description = Description,
                Id = Id
            };
            service.DeleteLocal(model);
            service.Delete(model.Id);
            var item = Functions.FirstOrDefault(i => i.Id == model.Id);
            Functions.Remove(item);
            Clean();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private void Clean()
        {
            Server = "";
            Action = "";
            Description = "";
            Id = "";
        }

        private void Go()
        {
            if (GoBool == false)
            {
                GoBool = true;
            }
            else
            {
                GoBool = false;
            }
        }

        private void Back()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
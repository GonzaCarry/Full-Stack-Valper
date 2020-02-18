using APPValper.Models;
using APPValper.Resources;
using APPValper.Services;
using APPValper.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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

        OptionsService service2 = new OptionsService();

        public Language Language { get; set; }
        public string Url { get; private set; }

        private string LanguageSelected;

        public string BrandTitle { get; set; }
        public string BrandNameText { get; set; }
        public string HeadquartersText { get; set; }
        public string FounderText { get; set; }
        public string SaveText { get; set; }
        public string ModifyText { get; set; }
        public string DeleteText { get; set; }
        public string ClearText { get; set; }
        public string ModelTitle { get; set; }
        public string BrandPickerText { get; set; }
        public string ModelNameText { get; set; }
        public string PowerText { get; set; }
        public string ColorText { get; set; }
        public string DoorsText { get; set; }


        public FunctionsViewModel()
        {
            Update();
            //ListView();
            //ListViewAsync();
            SaveCommand = new Command(async () => await Save(), () => !IsBusy);
            ModifyCommand = new Command(async () => await Modify(), () => !IsBusy);
            DeleteCommand = new Command(async () => await Delete(), () => !IsBusy);
            CleanCommand = new Command(Clean, () => !IsBusy);
            GoCommand = new Command(Go, () => !IsBusy);
            BackCommand = new Command(Back, () => !IsBusy);
            CheckLanguage();
        }

        private void CheckLanguage()
        {
            if (!string.IsNullOrEmpty(LanguageSelected))
            {
                if (LanguageSelected.Equals("Spanish"))
                {
                    BrandTitle = "Marcas de coche";
                    BrandNameText = "Nombre de la marca";
                    HeadquartersText = "Sede";
                    FounderText = "Fundador";
                    SaveText = "Guardar";
                    ModifyText = "Modificar";
                    DeleteText = "Eliminar";
                    ClearText = "Limpiar";
                    ModelTitle = "Modelos de coche";
                    BrandPickerText = "Nombre de la marca";
                    ModelNameText = "Nombre del modelo";
                    PowerText = "Caballos de potencia";
                    ColorText = "Color";
                    DoorsText = "Numero de puertas";
                }
                else
                {
                    BrandTitle = "Car´s brands";
                    BrandNameText = "Brand name";
                    HeadquartersText = "Headquarters";
                    FounderText = "Founder";
                    SaveText = "Save";
                    ModifyText = "Modify";
                    DeleteText = "Delete";
                    ClearText = "Clean";
                    ModelTitle = "Car's Models";
                    BrandPickerText = "Brand name";
                    ModelNameText = "Model name";
                    PowerText = "power";
                    ColorText = "Color";
                    DoorsText = "Door's number";
                }
            }
        }

        private void Update()
        {
            Url = service2.ConsultLocal().Url;
            LanguageSelected = service2.ConsultLanguage().Name;
        }

        private void ListView()
        {
            Functions = new ObservableCollection<Function>();
            //FunctionsAux = service.ConsultLocal();
            for (int i = 0; i < FunctionsAux.Count; i++)
            {
                Functions.Add(FunctionsAux[i]);
            }
        }

        private async Task ListViewAsync()
        {
            //FunctionsTask = service.Consult();
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
                //service.Save(model);
                //service.SaveLocal(model);
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
            //service.Modify(model);
            //service.ModifyLocal(model);
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
            //service.DeleteLocal(model);
            //service.Delete(model.Id);
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
            Application.Current.MainPage = new MainPage();
        }
    }
}

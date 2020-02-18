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
    class FunctionsViewModel : Crud
    {
        private Task<ObservableCollection<Car>> CarsTask { get; set; }
        private ObservableCollection<Car> CarsAux { get; set; }
        public ObservableCollection<Car> Cars { get; set; }

        private Task<ObservableCollection<Car>> BrandsTask { get; set; }
        private ObservableCollection<Car> BrandsAux { get; set; }
        public ObservableCollection<Car> Brands { get; set; }

        Car model;

        CarsService service = new CarsService();

        OptionsService service2 = new OptionsService();

        public string IDCar { get; set; }
        public string Model { get; set; }
        public string Power { get; set; }
        public string Color { get; set; }
        public int Ndoor { get; set; }

        public string IDBrand { get; set; }
        public string Name { get; set; }
        public string Headquarters { get; set; }
        public string Founder { get; set; }

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
            ListView();
            ListViewAsync();
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
                    BrandTitle = "Car's brand";
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
            Cars = new ObservableCollection<Car>();
            CarsAux = service.ConsultLocalCar();
            for (int i = 0; i < CarsAux.Count; i++)
            {
                Cars.Add(CarsAux[i]);
            }
        }

        private async Task ListViewAsync()
        {
            CarsTask = service.Consult();
            CarsAux = await CarsTask;
            for (int i = 0; i < CarsAux.Count; i++)
            {
                Console.WriteLine(CarsAux[i]);
                Cars.Add(CarsAux[i]);
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
            Guid idCar = Guid.NewGuid();
            model = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                /*BrandID = Id,*/
                Id = idCar.ToString()
            };
            if (string.IsNullOrEmpty(model.Model))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El modelo no puede ser nulo", "Aceptar");
            }
            else
            {
                service.Save(model);
                service.SaveLocalCar(model);
                Cars.Add(model);
                Clean();
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task Modify()
        {
            IsBusy = true;
            model = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                /*BrandID = Id,*/
                Id = IDCar
            };
            service.Modify(model);
            service.ModifyLocalCar(model);
            var item = Cars.FirstOrDefault(i => i.Id == model.Id);
            if (item != null)
            {
                item.Model = model.Model;
                item.Power = model.Power;
                item.Color = model.Color;
                item.Ndoor = model.Ndoor;
                item.BrandID = model.BrandID;
            }
            Clean();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task Delete()
        {
            IsBusy = true;
            model = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                /*BrandID = Id,*/
                Id = IDCar
            };
            service.DeleteLocalCar(model);
            service.Delete(model.Id);
            var item = Cars.FirstOrDefault(i => i.Id == model.Id);
            Cars.Remove(item);
            Clean();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private void Clean()
        {
            Model = "";
            Power = "";
            Color = "";
            Ndoor = 0;
            IDBrand = "";
            IDCar = "";

            Name = "";
            Headquarters = "";
            Founder = "";
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

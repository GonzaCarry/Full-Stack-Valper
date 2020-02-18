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
        private Task<ObservableCollection<Brand>> BrandsTask { get; set; }
        private ObservableCollection<Brand> BrandsAux { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }

        private Task<ObservableCollection<Car>> CarsTask { get; set; }
        private ObservableCollection<Car> CarsAux { get; set; }
        public ObservableCollection<Car> Cars { get; set; }

        Brand BrandModel;
        Car CarModel;

        FunctionsService service = new FunctionsService();

        OptionsService service2 = new OptionsService();

        public string IDBrand { get; set; }
        public string Model { get; set; }
        public string Power { get; set; }
        public string Color { get; set; }
        public int Ndoor { get; set; }

        public string IDCar { get; set; }
        public string Name { get; set; }
        public string Headquarters { get; set; }
        public string Founder { get; set; }

        public Language Language { get; set; }
        public string Url { get; private set; }

        private string LanguageSelected;

        public string CarTitle { get; set; }
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

        public Command SaveBrandCommand { get; set; }
        public Command ModifyBrandCommand { get; set; }
        public Command DeleteBrandCommand { get; set; }
        public Command CleanBrandCommand { get; set; }

        public Command SaveCarCommand { get; set; }
        public Command ModifyCarCommand { get; set; }
        public Command DeleteCarCommand { get; set; }
        public Command CleanCarCommand { get; set; }

        public FunctionsViewModel()
        {
            Update();
            
            CheckLanguage();
        }

        private void Brand()
        {
            ListViewBrand();
            ListViewAsyncBrand();
            SaveBrandCommand = new Command(async () => await SaveBrand(), () => !IsBusy);
            ModifyBrandCommand = new Command(async () => await ModifyBrand(), () => !IsBusy);
            DeleteBrandCommand = new Command(async () => await DeleteBrand(), () => !IsBusy);
            CleanBrandCommand = new Command(CleanBrand, () => !IsBusy);
        }

        private void Car()
        {
            ListViewCar();
            ListViewAsyncCar();
            SaveCarCommand = new Command(async () => await SaveCar(), () => !IsBusy);
            ModifyCarCommand = new Command(async () => await ModifyCar(), () => !IsBusy);
            DeleteCarCommand = new Command(async () => await DeleteCar(), () => !IsBusy);
            CleanCarCommand = new Command(CleanCar, () => !IsBusy);
        }

        private void CheckLanguage()
        {
            if (!string.IsNullOrEmpty(LanguageSelected))
            {
                if (LanguageSelected.Equals("Spanish"))
                {
                    CarTitle = "Marcas de coche";
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
                    BrandTitle = "Brand's car";
                    BrandNameText = "Brand name";
                    HeadquartersText = "Headquarters";
                    FounderText = "Founder";
                    SaveText = "Save";
                    ModifyText = "Modify";
                    DeleteText = "Delete";
                    ClearText = "Clean";
                    ModelTitle = "Brand's Models";
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
            Console.WriteLine(Url);
            LanguageSelected = service2.ConsultLanguage().Name;
        }

        private void ListViewBrand()
        {
            Brands = new ObservableCollection<Brand>();
            BrandsAux = service.ConsultLocalBrand();
            for (int i = 0; i < BrandsAux.Count; i++)
            {
                Brands.Add(BrandsAux[i]);
            }
        }

        private async Task ListViewAsyncBrand()
        {
            BrandsTask = service.ConsultBrand();
            BrandsAux = await BrandsTask;
            for (int i = 0; i < BrandsAux.Count; i++)
            {
                Console.WriteLine(BrandsAux[i]);
                Brands.Add(BrandsAux[i]);
            }
        }

        private async Task SaveBrand()
        {
            IsBusy = true;
            Guid idBrand = Guid.NewGuid();
            BrandModel = new Brand()
            {
                Name = Name,
                Headquarters = Headquarters,
                Founder = Founder,
                IdBrand = idBrand.ToString()
            };
            if (string.IsNullOrEmpty(BrandModel.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El modelo no puede ser nulo", "Aceptar");
            }
            else
            {
                service.SaveBrand(BrandModel);
                service.SaveLocalBrand(BrandModel);
                Brands.Add(BrandModel);
                CleanBrand();
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task ModifyBrand()
        {
            IsBusy = true;
            BrandModel = new Brand()
            {
                Name = Name,
                Headquarters = Headquarters,
                Founder = Founder,
                IdBrand = IDBrand
            };
            service.ModifyBrand(BrandModel);
            service.ModifyLocalBrand(BrandModel);
            var item = Brands.FirstOrDefault(i => i.IdBrand == BrandModel.IdBrand);
            if (item != null)
            {
                item.Name = BrandModel.Name;
                item.Headquarters = BrandModel.Headquarters;
                item.Founder = BrandModel.Founder;
                item.IdBrand = BrandModel.IdBrand;
            }
            CleanBrand();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task DeleteBrand()
        {
            IsBusy = true;
            BrandModel = new Brand()
            {
                Name = Name,
                Headquarters = Headquarters,
                Founder = Founder,
                IdBrand = IDBrand
            };
            service.DeleteLocalBrand(BrandModel);
            service.DeleteBrand(BrandModel.IdBrand);
            var item = Brands.FirstOrDefault(i => i.IdBrand == BrandModel.IdBrand);
            Brands.Remove(item);
            CleanBrand();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private void CleanBrand()
        {
            Model = "";
            Power = "";
            Color = "";
            Ndoor = 0;
            IDCar = "";
            IDBrand = "";
            Name = "";
            Headquarters = "";
            Founder = "";
        }

        private void ListViewCar()
        {
            Cars = new ObservableCollection<Car>();
            CarsAux = service.ConsultLocalCar();
            for (int i = 0; i < CarsAux.Count; i++)
            {
                Cars.Add(CarsAux[i]);
            }
        }

        private async Task ListViewAsyncCar()
        {
            CarsTask = service.ConsultCar();
            CarsAux = await CarsTask;
            for (int i = 0; i < CarsAux.Count; i++)
            {
                Console.WriteLine(CarsAux[i]);
                Cars.Add(CarsAux[i]);
            }
        }

        private async Task SaveCar()
        {
            IsBusy = true;
            Guid idCar = Guid.NewGuid();
            CarModel = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                /*BrandID = Id,*/
                CarID = idCar.ToString()
            };
            if (string.IsNullOrEmpty(CarModel.Model))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El modelo no puede ser nulo", "Aceptar");
            }
            else
            {
                service.SaveCar(CarModel);
                service.SaveLocalCar(CarModel);
                Cars.Add(CarModel);
                CleanCar();
            }
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task ModifyCar()
        {
            IsBusy = true;
            CarModel = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                /*BrandID = Id,*/
                CarID = IDCar
            };
            service.ModifyCar(CarModel);
            service.ModifyLocalCar(CarModel);
            var item = Cars.FirstOrDefault(i => i.CarID == CarModel.CarID);
            if (item != null)
            {
                item.Model = CarModel.Model;
                item.Power = CarModel.Power;
                item.Color = CarModel.Color;
                item.Ndoor = CarModel.Ndoor;
                item.CarID = CarModel.CarID;
            }
            CleanCar();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private async Task DeleteCar()
        {
            IsBusy = true;
            CarModel = new Car()
            {
                Model = Model,
                Power = Power,
                Color = Color,
                Ndoor = Ndoor,
                /*BrandID = Id,*/
                CarID = IDCar
            };
            service.DeleteLocalCar(CarModel);
            service.DeleteCar(CarModel.CarID);
            var item = Cars.FirstOrDefault(i => i.CarID == CarModel.CarID);
            Cars.Remove(item);
            CleanCar();
            await Task.Delay(2000);
            IsBusy = false;
        }

        private void CleanCar()
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

    }
}

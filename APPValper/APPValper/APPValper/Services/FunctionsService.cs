using APPValper.Services;
using APPValper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace APPValper.Resources
{
    public class CarsService
    {
        public ObservableCollection<Car> Cars { get; set; }
        private string apiUrl;

        public CarsService()
        {
            using (var data = new DataAccess())
            {
                apiUrl = data.GetConnection().Url + "/api/Cars";
            }
            if (Cars == null)
            {
                Cars = new ObservableCollection<Car>();
            }
        }

        public async System.Threading.Tasks.Task<ObservableCollection<Car>> Consult()
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient();
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        Cars = JsonConvert.DeserializeObject<ObservableCollection<Car>>(result);
                    }
                }
                return Cars;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ObservableCollection<Car> ConsultLocalCar()
        {
            using (var data = new DataAccess())
            {
                var list = data.GetCars();
                foreach (var item in list)
                    Cars.Add(item);
            }
            return Cars;
        }

        public async void Save(Car model)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient();
                    var send = Newtonsoft.Json.JsonConvert.SerializeObject(model,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
                    request.Content = new StringContent(send, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                    HttpResponseMessage response = await client.SendAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveLocalCar(Car model)
        {
            using (var data = new DataAccess())
            {
                data.InsertCar(model);
            }
        }

        public async void Modify(Car model)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient();
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    Uri apiUrl2 = new Uri(string.Format(apiUrl + "/{0}", model.Id));
                    HttpResponseMessage response = await client.PutAsync(apiUrl2, content);
                    Console.WriteLine(response.IsSuccessStatusCode);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifyLocalCar(Car model)
        {
            using (var data = new DataAccess())
            {
                data.ModifyCar(model);
            }
        }

        public async void Delete(string idCar)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient();
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + idCar);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteLocalCar(Car model)
        {
            using (var data = new DataAccess())
            {
                data.DeleteCar(model);
            }
        }

        private HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["token"].ToString());
            client.Timeout = TimeSpan.FromMinutes(10);
            client.Timeout = new TimeSpan(0, 0, 0, 0, -1);
            return client;
        }

    }
}
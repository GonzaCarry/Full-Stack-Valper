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

namespace APPValper.Resources
{
    public class FunctionsService
    {
        public ObservableCollection<Function> Functions { get; set; }
        private string apiUrl;

        public FunctionsService()
        {
            using (var data = new DataAccess())
            {
                apiUrl = data.GetConnection().Url + "/api/Functions";
            }
            if (Functions == null)
            {
                Functions = new ObservableCollection<Function>();
            }
        }

        public async System.Threading.Tasks.Task<ObservableCollection<Function>> Consult()
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
                        Functions = JsonConvert.DeserializeObject<ObservableCollection<Function>>(result);
                    }
                }
                return Functions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ObservableCollection<Function> ConsultLocal()
        {
            using (var data = new DataAccess())
            {
                var list = data.GetFunctions();
                foreach (var item in list)
                    Functions.Add(item);
            }
            return Functions;
        }

        public async void Save(Function model)
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

        public void SaveLocal(Function model)
        {
            using (var data = new DataAccess())
            {
                data.InsertFunction(model);
            }
        }

        public async void Modify(Function model)
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

        public void ModifyLocal(Function model)
        {
            using (var data = new DataAccess())
            {
                data.ModifyFunction(model);
            }
        }

        public async void Delete(string idFunction)
        {
            try
            {
                HttpClient client;
                using (client = new HttpClient())
                {
                    client = CreateClient();
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl + "/" + idFunction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteLocal(Function model)
        {
            using (var data = new DataAccess())
            {
                data.DeleteFunction(model);
            }
        }

        private HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["token"].ToString());
            client.Timeout = TimeSpan.FromMinutes(10);
            client.Timeout = new TimeSpan(0, 0, 0, 0, -1);
            return client;
        }

    }
}
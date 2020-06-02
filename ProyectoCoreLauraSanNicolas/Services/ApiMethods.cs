using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProyectoCoreLauraSanNicolas.Models.ViewModels;
using ProyectoDineroNuGet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public class ApiMethods
    {
        private String url;
        private MediaTypeWithQualityHeaderValue header;

        public ApiMethods()
        {
            this.url = "https://proyectodineroapilsn.azurewebsites.net";
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        public async Task<String> GetToken(LoginDTO userlogin)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                String json = JsonConvert.SerializeObject(userlogin);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                String apiRequest = "/Auth/Login";
                HttpResponseMessage response = await client.PostAsync(apiRequest, content);
                if (response.IsSuccessStatusCode)
                {
                    String data = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(data);
                    String token = jsonObject.GetValue("response").ToString();
                    return token;
                } else
                {
                    return null;
                }
            }
        }

        public async Task<T> CallApiWithoutAuth<T>(String request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                T datos = await response.Content.ReadAsAsync<T>();
                return (T)Convert.ChangeType(datos, typeof(T));
            }
        }

        public async Task<T> CallApiWithAuth<T>(String request, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
                T datos = await response.Content.ReadAsAsync<T>();
                return (T)Convert.ChangeType(datos, typeof(T));
            }
        }

        public async Task<T> AddUser<T>(UserDTO userDTO)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "/api/User/AddUser";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                String json = JsonConvert.SerializeObject(userDTO);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                T datos = await response.Content.ReadAsAsync<T>();
                return (T)Convert.ChangeType(datos, typeof(T));
            }
        }

        public async Task DeleteUser(int UserId, String token)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "/api/User/DeleteUser/"+UserId;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                await client.DeleteAsync(request);
            }
            
        }

        public async Task EditUser(String Name, String Surname, String Email, String Username, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/User/EditUser";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                UserDTO user = new UserDTO();
                user.Name = Name;
                user.Surname = Surname;
                user.Email = Email;
                user.Username = Username;
                String json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task<String> GetBlobToken()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                String apiRequest = "/Auth/GetBlobToken";
                HttpResponseMessage response = await client.GetAsync(apiRequest);
                String datos = await response.Content.ReadAsAsync<String>();
                if (response.IsSuccessStatusCode)
                {
                    return (String)Convert.ChangeType(datos, typeof(String));
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Windows.UI.Popups;

namespace SmartCityApp.Services
{
    public class UserService
    {

        public async Task<string> ConnectionUser(string login, string password)
        {
            var appData = Windows.Storage.ApplicationData.Current;
            var localSetting = appData.LocalSettings;

            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            /*var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("Username", login));
            postData.Add(new KeyValuePair<string, string>("Password ", password));
            postData.Add(new KeyValuePair<string, string>("grant_type", "password"));*/

            var form = new Dictionary<string, string>
            {
                {"username",login},
                {"password",password},
                {"grant_type","password"},
            };

            var response = client.PostAsync(new Uri("http://ubinamapi.azurewebsites.net/token"), new FormUrlEncodedContent(form)).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var tokenJObject = JObject.Parse(responseStream);

                localSetting.Values["token"] = tokenJObject.SelectToken("access_token").Value<string>();
                localSetting.Values["username"] = tokenJObject.SelectToken("userName").Value<string>();
                return "ok";
            }
            else
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(responseStream);
                var errorMessage = jObject.SelectToken("error_description").Value<string>();
                return errorMessage;
            }
        }

        public async Task<string> AddUser(User user)
        {

            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var result = client.PostAsync("http://ubinamapi.azurewebsites.net/api/Account/Register", content).Result;

            if (result.IsSuccessStatusCode)
            {
                return "ok";
            }
            else
            {
                return result.StatusCode.ToString();
            }
        }
    }
}

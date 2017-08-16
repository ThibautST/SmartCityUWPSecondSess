using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace SmartCityApp.Services
{
    public class RouteService
    {

        public async Task<List<Route>> GetAllRoutes()
        {
            var appData = Windows.Storage.ApplicationData.Current;
            var localSettings = appData.LocalSettings;

            List<Route> routeList = new List<Route>();

            HttpClient client = new HttpClient();


            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)localSettings.Values["token"]);

            var response = client.GetAsync("http://ubinamapi.azurewebsites.net/api/GuidedTours").Result;

            if (response.IsSuccessStatusCode)
            {

                var routes = response.Content.ReadAsAsync<List<Route>>().Result;

                foreach (var r in routes)
                {
                    var route = new Route()
                    {
                        Id_GuidedTour = r.Id_GuidedTour,
                        GuidedTourName = r.GuidedTourName,
                        Distance = r.Distance,
                        Transport = r.Transport,
                        PlaceToEat = r.PlaceToEat,
                        Category = r.Category
                    };

                   routeList.Add(route);

                }

            }

            return routeList;

        }

        public async Task<Route> GetRoute(int id)
        {
            var appData = Windows.Storage.ApplicationData.Current;
            var localSettings = appData.LocalSettings;

            HttpClient client = new HttpClient();


            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)localSettings.Values["token"]);

            try
            {
                var response = client.GetAsync("http://ubinamapi.azurewebsites.net/api/GuidedToursWithPlaces/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var r = response.Content.ReadAsAsync<Route>().Result;

                    var route = new Route
                    {
                        Id_GuidedTour = r.Id_GuidedTour,
                        GuidedTourName = r.GuidedTourName,
                        Distance = r.Distance,
                        TouristPlaces = r.TouristPlaces
                    };

                    return route;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                var dialogue = new MessageDialog(e.ToString());
                dialogue.ShowAsync();
                return null;
            }
        }

    }
}

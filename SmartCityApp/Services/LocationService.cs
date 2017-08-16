using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartCityApp.Services
{
   public  class LocationService
    {
        public async Task<List<Location>> GetAllLocations()
        {
            var appData = Windows.Storage.ApplicationData.Current;
            var localSettings = appData.LocalSettings;

            List<Location> LocationsList = new List<Location>();

            HttpClient client = new HttpClient();


            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (String)localSettings.Values["token"]);

            var response = client.GetAsync("http://ubinamapi.azurewebsites.net/api/TouristPlaces").Result;

            if (response.IsSuccessStatusCode)
            {
                var places = response.Content.ReadAsAsync<List<Location>>().Result;

                foreach (var p in places)
                {

                    var loc = new Location()
                    {
                        Id_TouristPlace = p.Id_TouristPlace,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                        TouristPlaceName = p.TouristPlaceName,
                        Address = p.Address,
                        Description = p.Description,
                        Time = p.Time,
                        Id_Photo = p.Id_Photo,
                        Price = p.Price
                    };

                        LocationsList.Add(loc);

                }
            }
            return LocationsList;
        }

    }
}

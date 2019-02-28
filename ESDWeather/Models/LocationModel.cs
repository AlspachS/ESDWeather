using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESDWeather.Models
{
    public class LocationModel
    {
        private static string GeoIP_APIkey = "f9f843c6ac281262eb7980f09c84db23";
        public LocationModel() { }
        public LocationModel(string location)
        {
            City = location;
        }

        public string City { get; set; }

        public static async Task<LocationModel> FindLocationFromIP(string ipAddress, HttpClient request)
        {
            string response = await request.GetStringAsync($"http://api.ipstack.com/{ipAddress}?access_key={GeoIP_APIkey}");
            
            return JsonConvert.DeserializeObject<LocationModel>(response);
        }
    }
}

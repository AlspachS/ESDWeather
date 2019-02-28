using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESDWeather.Models
{
    public class WeatherModel
    {
        private static string OpenWeatherMap_APIkey = "4fd2bfa400b9552f6ef27ced751c4b33";

        public string Weather { get; set; }

        public string Description { get; set; }

        public int Temperature { get; set; }

        public string LastUpdated { get; set; }

        public static async Task<WeatherModel> FindWeatherFromLocation(string location, HttpClient request)
        {
            WeatherModel weather = new WeatherModel();
            try
            {
                string response = await request.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={location}&units=imperial&appid={OpenWeatherMap_APIkey}");
                JObject things = JObject.Parse(response);
                weather.Temperature = (int)things.SelectToken("main.temp");
                weather.Weather = (string)things.SelectToken("weather[0].main");
                weather.Description = (string)things.SelectToken("weather[0].description");
                long updated = (long)things.SelectToken("dt");
                weather.LastUpdated = DateTimeOffset.FromUnixTimeSeconds(updated).ToLocalTime().TimeOfDay.ToString();
            }
            catch (Exception)
            {
                weather.Temperature = 0;
                weather.Weather = "Error";
                weather.Description = "City not found";
                weather.LastUpdated = DateTime.Now.ToString("HH:mm:ss");
            }

            return weather;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESDWeather.Models;
using System.Text.Encodings.Web;
using ESDWeather.ViewModels;
using System.Net.Http;

namespace ESDWeather.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            WeatherAtLocationViewModel userWeather = new WeatherAtLocationViewModel();

            string ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            ip = "68.186.11.102";

            LocationModel userLocation = await LocationModel.FindLocationFromIP(ip, client);
            WeatherModel locationWeather = await WeatherModel.FindWeatherFromLocation(userLocation.City, client);

            userWeather.location = userLocation;
            userWeather.weather = locationWeather;

            return View(userWeather);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string location)
        {
            WeatherAtLocationViewModel searchedWeather = new WeatherAtLocationViewModel();

            LocationModel searchedLocation = new LocationModel(location);
            WeatherModel locationWeather = await WeatherModel.FindWeatherFromLocation(location, client);

            searchedWeather.location = searchedLocation;
            searchedWeather.weather = locationWeather;

            return View(searchedWeather);
        }
    }
}

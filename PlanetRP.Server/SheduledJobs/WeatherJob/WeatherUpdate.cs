using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.SheduledJobs.WeatherSheduledJob
{
    public class WeatherUpdate
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherUpdate(string apiKey)
        {
            _apiKey = apiKey;

            _httpClient = new HttpClient 
            {
                BaseAddress = new Uri($"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q=Los-Angeles&aqi=no") 
            };
        }
    }
}

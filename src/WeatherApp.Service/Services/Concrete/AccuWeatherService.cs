using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using WeatherApp.Service.Services.Abstract;
using Newtonsoft.Json;
using WeatherApp.Service.ResultTypes;

namespace WeatherApp.Service.Services.Concrete
{
    public class AccuWeatherService : BaseWeatherService, IWeatherService
    {
        public AccuWeatherService(IRestClient restClient, string apiUrl) : base(restClient, apiUrl) { }

        public WeatherApiResult GetWeather(string location)
        {
            return base.GetWeather<AccuweatherResult>(location);
        }
    }
}

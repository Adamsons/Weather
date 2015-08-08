using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WeatherApp.Service.ResultTypes;
using WeatherApp.Service.Services.Abstract;

namespace WeatherApp.Service.Services.Concrete
{
    public class BbcWeatherService : BaseWeatherService, IWeatherService
    {
        public BbcWeatherService(IRestClient restClient, string apiUrl) : base(restClient, apiUrl) { }

        public async Task<WeatherApiResult> GetWeather(string location)
        {
            return await base.GetWeather<BbcWeatherResult>(location);
        }
    }
}

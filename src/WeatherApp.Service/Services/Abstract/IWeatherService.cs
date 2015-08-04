using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using WeatherApp.Service.ResultTypes;

namespace WeatherApp.Service.Services.Abstract
{
    public interface IWeatherService
    {
        WeatherApiResult GetWeather(string location);
    }
}

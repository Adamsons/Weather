using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using UnitsNet.Units;
using WeatherApp.Service.ResultTypes;

namespace WeatherApp.Tests
{
    public class TestData
    {
        public static WeatherApiResult GetTestWeatherResult(TemperatureUnit tempUnit, SpeedUnit speedUnit)
        {
            var random = new Random();

            return new WeatherApiResult
            {
                Location = "Liverpool",
                Temperature = random.Next(0, 40),
                WindSpeed = random.Next(0, 40),
                TemperatureUnit = tempUnit,
                WindSpeedUnit = speedUnit
            };
        }

        public static WeatherApiResult GetTestWeatherResult(double temp, double wind)
        {
            return new WeatherApiResult
            {
                Location = "Liverpool",
                Temperature = temp,
                WindSpeed = wind,
                TemperatureUnit = TemperatureUnit.DegreeCelsius,
                WindSpeedUnit = SpeedUnit.MilePerHour
            };
        }

        public static IRestResponse MockResult(string content = "")
        {
            var response = new RestResponse { Content = content };
            return response;
        }
    }
}

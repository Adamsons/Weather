using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using UnitsNet;
using UnitsNet.Units;
using WeatherApp.Service.ResultTypes;
using WeatherApp.Service.Services.Abstract;

namespace WeatherApp.Service.Extensions
{
    public static class Extensions
    {
        public static WeatherApiResult AverageWeatherResults(this IEnumerable<WeatherApiResult> weatherResults, TemperatureUnit temperatureMeasurement, SpeedUnit windSpeedMeasurement)
        {
            if (weatherResults == null) throw new ArgumentNullException("weatherResults");

            var weatherApiResults = weatherResults.ToList();

            if (weatherApiResults.Count == 0)
                return null;

            foreach (var weatherApiResult in weatherApiResults)
            {
                weatherApiResult.ConvertTemperature(temperatureMeasurement);
                weatherApiResult.ConvertWindSpeed(windSpeedMeasurement);
            }

            return new WeatherApiResult
            {
                Location = weatherApiResults.First().Location,
                TemperatureUnit = temperatureMeasurement,
                WindSpeedUnit = windSpeedMeasurement,
                WindSpeed = weatherApiResults.Average(o => o.WindSpeed),
                Temperature = weatherApiResults.Average(o => o.Temperature)
            };
        }

        public static double Round(this double d)
        {
            return Math.Round(d);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WeatherApp.Configuration
{
    public class ApiConfig
    {
        private const string DefaultAccuWeatherApiUrl = "http://localhost:18888/api/";
        private const string DefaultBbcWeatherApiUrl = "http://localhost:17855/api/";

        public static string AccuWeatherApiUrl
        {
            get { return ConfigurationManager.AppSettings["accuweatherApiUrl"] ?? DefaultAccuWeatherApiUrl; }
        }

        public static string BbcWeatherApiUrl
        {
            get { return ConfigurationManager.AppSettings["bbcWeatherApiUrl"] ?? DefaultBbcWeatherApiUrl; }
        }
    }
}
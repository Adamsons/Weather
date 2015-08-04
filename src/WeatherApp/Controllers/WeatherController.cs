using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitsNet.Units;
using WeatherApp.Models;
using WeatherApp.Service.Extensions;
using WeatherApp.Service.ResultTypes;
using WeatherApp.Service.Services.Abstract;

namespace WeatherApp.Controllers
{
    [RoutePrefix("api/weather")]
    public class WeatherController : ApiController
    {
        private ICollection<IWeatherService> _weatherServices;
       
        public WeatherController(ICollection<IWeatherService> weatherServices)
        {
            _weatherServices = weatherServices;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetWeather([FromUri]string Location, [FromUri]string Temperature, [FromUri]string WindSpeed)
        {
            if (Location == null || Temperature == null || WindSpeed == null)
                return BadRequest();

            TemperatureUnit temperatureUnit;
            SpeedUnit windSpeedUnit;

            if (TemperatureUnit.TryParse(Temperature, out temperatureUnit) &&
                SpeedUnit.TryParse(WindSpeed, out windSpeedUnit))
            {
                List<WeatherApiResult> apiResults = GetWeatherResults(Location);
                WeatherApiResult resultAverage = apiResults.AverageWeatherResults(temperatureUnit, windSpeedUnit);

                return Json(resultAverage);
            }

            return BadRequest();
        }

        private List<WeatherApiResult> GetWeatherResults(string location)
        {
            List<WeatherApiResult> apiResults = new List<WeatherApiResult>();

            foreach (var service in _weatherServices)
            {
                var result = service.GetWeather(location);

                if (result != null)
                    apiResults.Add(result);
            }

            return apiResults;
        }
    }
}

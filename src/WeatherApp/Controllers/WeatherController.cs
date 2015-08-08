using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IWeatherAggregatorService _weatherAggregator;
       
        public WeatherController(IWeatherAggregatorService weatherAggregator)
        {
            _weatherAggregator = weatherAggregator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetWeather([FromUri]string Location, [FromUri]string Temperature, [FromUri]string WindSpeed)
        {
            if (Location == null || Temperature == null || WindSpeed == null)
                return BadRequest();

            TemperatureUnit temperatureUnit;
            SpeedUnit windSpeedUnit;

            if (TemperatureUnit.TryParse(Temperature, out temperatureUnit) &&
                SpeedUnit.TryParse(WindSpeed, out windSpeedUnit))
            {
                var apiResults = await _weatherAggregator.GetWeatherResults(Location);
                WeatherApiResult resultAverage = apiResults.AverageWeatherResults(temperatureUnit, windSpeedUnit);

                return Json(resultAverage);
            }

            return BadRequest();
        }
    }
}

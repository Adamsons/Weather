using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Service.ResultTypes;
using WeatherApp.Service.Services.Abstract;

namespace WeatherApp.Service.Services.Concrete
{
    public class WeatherAggregatorService : IWeatherAggregatorService
    {
        private readonly IEnumerable<IWeatherService> _weatherServices; 

        public WeatherAggregatorService(IEnumerable<IWeatherService> services)
        {
            _weatherServices = services;
        }

        public async Task<IEnumerable<WeatherApiResult>> GetWeatherResults(string location)
        {
            var apiResults = _weatherServices.Select( o => o.GetWeather(location));
            return await Task.WhenAll(apiResults);
        }
    }
}

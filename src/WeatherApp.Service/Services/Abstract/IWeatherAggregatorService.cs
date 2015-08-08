using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Service.ResultTypes;

namespace WeatherApp.Service.Services.Abstract
{
    public interface IWeatherAggregatorService
    {
        Task<IEnumerable<WeatherApiResult>> GetWeatherResults(string location);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WeatherApp.Service.ResultTypes;

namespace WeatherApp.Service.Services.Concrete
{
    public abstract class BaseWeatherService
    {
        private IRestClient _restClient;
        private const int TimeoutMiliseconds = 3000;

        public BaseWeatherService(IRestClient restClient, string apiUrl)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(apiUrl);
        }

        protected WeatherApiResult GetWeather<T>(string location) where T : WeatherApiResult
        {
            IRestRequest request = new RestRequest("weather/{location}", Method.GET);
            request.AddUrlSegment("location", location);
            request.Timeout = TimeoutMiliseconds;

            var result = _restClient.Execute(request);

            if (result.ErrorException != null)
                Debug.WriteLine(result.ErrorException);

            return JsonConvert.DeserializeObject<T>(result.Content);
        }
    }
}

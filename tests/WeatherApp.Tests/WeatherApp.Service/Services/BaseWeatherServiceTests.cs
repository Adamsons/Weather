using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RestSharp;
using WeatherApp.Service.Services.Abstract;
using WeatherApp.Service.Services.Concrete;
using Xunit;

namespace WeatherApp.Tests.WeatherApp.Service.Services
{
    public class BaseWeatherServiceTests
    {
        private IWeatherService _weatherService;
        private const string Location = "Liverpool";
        private const string ApiUrl = "http://localhost:12345";

        [Fact]
        public async void GetWeatherCallsExecute()
        {
            var restClient = new Mock<IRestClient>();
            restClient.Setup(o => o.Execute(It.IsAny<IRestRequest>())).Returns(TestData.MockResult());
            restClient.Setup(o => o.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(new RestResponse());

            _weatherService = new BbcWeatherService(restClient.Object, ApiUrl);
            await _weatherService.GetWeather(Location);

            restClient.Verify(o => o.ExecuteTaskAsync(It.IsAny<IRestRequest>()));
        }
    }
}

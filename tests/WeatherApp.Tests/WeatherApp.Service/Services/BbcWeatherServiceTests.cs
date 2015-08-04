using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RestSharp;
using WeatherApp.Service.Services.Abstract;
using WeatherApp.Service.Services.Concrete;
using Xunit;

namespace WeatherApp.Tests.WeatherApp.Service.Services
{
    public class BbcWeatherServiceTests
    {
        private IWeatherService _weatherService;
        private const string Location = "Liverpool";
        private const string ApiUrl = "http://localhost:12345";

        [Fact]
        public void ConstructorSetsRestClientUrl()
        {
            var restClient = new RestClient();

            _weatherService = new BbcWeatherService(restClient, ApiUrl);
            restClient.BaseUrl.ShouldBeEquivalentTo(new Uri(ApiUrl));
        }

        [Fact]
        public void GetWeatherCallsExecute()
        {
            var restClient = new Mock<IRestClient>();
            restClient.Setup(o => o.Execute(It.IsAny<IRestRequest>())).Returns(TestData.MockResult());

            _weatherService = new BbcWeatherService(restClient.Object, ApiUrl);
            _weatherService.GetWeather(Location);

            restClient.Verify(o => o.Execute(It.IsAny<IRestRequest>()));
        }

        [Fact]
        public void GetWeatherDeserializesResult()
        {
            var restClient = new Mock<IRestClient>();
            var json = "{\"Location\":\"Liverpool\",\"TemperatureCelsius\":22.0,\"WindSpeedKph\":6.0}";
            restClient.Setup(o => o.Execute(It.IsAny<IRestRequest>())).Returns(TestData.MockResult(json));

            _weatherService = new BbcWeatherService(restClient.Object, ApiUrl);

            var result = _weatherService.GetWeather(Location);
            result.Should().NotBeNull();
        }
    }
}

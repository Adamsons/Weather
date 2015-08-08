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
    public class AccuWeatherServiceTests
    {
        private IWeatherService _weatherService;
        private const string Location = "Liverpool";
        private const string ApiUrl = "http://localhost:12345";

        [Fact]
        public void ConstructorSetsClientBaseUrl()
        {
            const string apiUrl = "http://localhost/api";
            var mockClient = new Mock<IRestClient>();

            IWeatherService service = new AccuWeatherService(mockClient.Object, apiUrl);
            mockClient.VerifySet(o => o.BaseUrl = new Uri(apiUrl));
        }

        [Fact]
        public void GetWeatherDeserializesResult()
        {
            var restClient = new Mock<IRestClient>();
            var json = "{\"Location\":\"Liverpool\",\"TemperatureFahrenheit\":22.0,\"WindSpeedMph\":6.0}";
            restClient.Setup(o => o.Execute(It.IsAny<IRestRequest>())).Returns(TestData.MockResult(json));

            _weatherService = new AccuWeatherService(restClient.Object, ApiUrl);

            var result = _weatherService.GetWeather(Location);
            result.Should().NotBeNull();
        }
    }
}

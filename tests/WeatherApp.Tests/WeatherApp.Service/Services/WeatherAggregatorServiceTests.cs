using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using WeatherApp.Service.ResultTypes;
using WeatherApp.Service.Services.Abstract;
using WeatherApp.Service.Services.Concrete;
using Xunit;

namespace WeatherApp.Tests.WeatherApp.Service.Services
{
    public class WeatherAggregatorServiceTests
    {
        private const string Location = "Liverpool";
        private IWeatherAggregatorService _service;

        [Fact]
        public async void ShouldReturnWeatherResults()
        {
            var serviceA = new Mock<IWeatherService>();
            var serviceB = new Mock<IWeatherService>();

            serviceA.Setup(o => o.GetWeather(Location)).ReturnsAsync(new AccuweatherResult());
            serviceB.Setup(o => o.GetWeather(Location)).ReturnsAsync(new BbcWeatherResult());

            var listOfServices = new List<IWeatherService>() { serviceA.Object, serviceB.Object };
            _service = new WeatherAggregatorService(listOfServices);

            var result = await _service.GetWeatherResults(Location);

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.All(o => o != null).Should().BeTrue();
        }
    }
}

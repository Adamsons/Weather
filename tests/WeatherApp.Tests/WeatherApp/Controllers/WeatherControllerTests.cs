using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Moq;
using WeatherApp.Controllers;
using WeatherApp.Models;
using WeatherApp.Service.ResultTypes;
using WeatherApp.Service.Services.Abstract;
using Xunit;

namespace WeatherApp.Tests.WeatherApp.Controllers
{
    public class WeatherControllerTests
    {
        private const string Location = "Liverpool";

        [Fact]
        public async void NullModelReturnsBadRequest()
        {
            var controller = new WeatherController(null);
            var result = await controller.GetWeather(null, null, null);
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void ReturnsJsonResult()
        {
            WeatherApiResult resultA = TestData.GetTestWeatherResult(10, 15);
            WeatherApiResult resultB = TestData.GetTestWeatherResult(11, 20);
            var results = new List<WeatherApiResult> {resultA, resultB};

            var service = new Mock<IWeatherAggregatorService>();
            service.Setup(o => o.GetWeatherResults(Location)).ReturnsAsync(results);

            var controller = new WeatherController(service.Object);

            var result = await controller.GetWeather(Location, "DegreeCelsius", "KilometerPerHour");

            result.Should().BeOfType<JsonResult<WeatherApiResult>>();

            var jsonResult = result as JsonResult<WeatherApiResult>;
            jsonResult.Content.Should().NotBeNull();
        }

        [Fact]
        public async void CantParseUnitsOfMeasurementReturnsBadRequest()
        {
            var controller = new WeatherController(null);

            var result = await controller.GetWeather(Location, "a", "b");
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}

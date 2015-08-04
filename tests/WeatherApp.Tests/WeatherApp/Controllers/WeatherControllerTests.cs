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
        public void NullModelReturnsBadRequest()
        {
            var controller = new WeatherController(null);
            var result = controller.GetWeather(null, null, null);
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void ReturnsJsonResult()
        {
            var serviceA = new Mock<IWeatherService>();
            var serviceB = new Mock<IWeatherService>();

            WeatherApiResult resultA = TestData.GetTestWeatherResult(10, 15);
            WeatherApiResult resultB = TestData.GetTestWeatherResult(11, 20);

            serviceA.Setup(o => o.GetWeather(Location)).Returns(resultA);
            serviceB.Setup(o => o.GetWeather(Location)).Returns(resultB);

            var services = new List<IWeatherService> { serviceA.Object, serviceB.Object };
            var controller = new WeatherController(services);

            var result = controller.GetWeather(Location, "DegreeCelsius", "KilometerPerHour");
            
            result.Should().BeOfType<JsonResult<WeatherApiResult>>();

            var jsonResult = result as JsonResult<WeatherApiResult>;
            jsonResult.Content.Should().NotBeNull();
        }

        [Fact]
        public void CantParseUnitsOfMeasurementReturnsBadRequest()
        {
            var controller = new WeatherController(null);

            var result = controller.GetWeather(Location, "a", "b");
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}

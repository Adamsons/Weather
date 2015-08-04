using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using UnitsNet;
using UnitsNet.Units;
using WeatherApp.Service.ResultTypes;
using Xunit;

namespace WeatherApp.Tests.WeatherApp.Service.ResultTypes
{
    public class WeatherResultTests
    {
        [Fact]
        public void ConstructorSetsCreatedTime()
        {
            var model = new WeatherApiResult();
            model.CreatedStamp.Should().NotBeNull(); 
        }

        [Fact]
        public void RoundsCorrectly()
        {
            var model = new WeatherApiResult()
            {
                Temperature = 10.6,
                WindSpeed = 9.9
            };

            model.Round();
            model.WindSpeed.ShouldBeEquivalentTo(10);
            model.Temperature.ShouldBeEquivalentTo(11);

        }

        [Fact]
        public void DeserializeBbcWeatherResult()
        {
            var json = "{\"Location\":\"Liverpool\",\"TemperatureCelsius\":22.0,\"WindSpeedKph\":6.0}";

            var result = JsonConvert.DeserializeObject<BbcWeatherResult>(json);

            result.Should().NotBeNull();
            result.Location.Should().Be("Liverpool");
            result.Temperature.Should().Be(22);
            result.WindSpeed.Should().Be(6);
        }

        [Fact]
        public void SetsUnitsOfMeasurement()
        {
            var json = "{\"Location\":\"Liverpool\",\"TemperatureCelsius\":22.0,\"WindSpeedKph\":6.0}";

            var result = JsonConvert.DeserializeObject<BbcWeatherResult>(json);

            result.TemperatureUnit.Should().Be(TemperatureUnit.DegreeCelsius);
            result.WindSpeedUnit.Should().Be(SpeedUnit.KilometerPerHour);
        }

        [Theory,
        InlineData(TemperatureUnit.DegreeCelsius, TemperatureUnit.DegreeCelsius, SpeedUnit.KilometerPerHour, SpeedUnit.MilePerHour),
        InlineData(TemperatureUnit.DegreeFahrenheit, TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour, SpeedUnit.KilometerPerHour),
        InlineData(TemperatureUnit.DegreeCelsius, TemperatureUnit.DegreeFahrenheit, SpeedUnit.KilometerPerHour, SpeedUnit.MilePerHour)]
        public void ConvertsUnitsOfMeasurement(TemperatureUnit tempBefore, TemperatureUnit tempAfter, SpeedUnit windBefore, SpeedUnit windAfter)
        {
            var weatherResult = TestData.GetTestWeatherResult(tempBefore, windBefore);

            weatherResult.ConvertTemperature(tempAfter);
            weatherResult.ConvertWindSpeed(windAfter);

            weatherResult.TemperatureUnit.Should().Be(tempAfter);
            weatherResult.WindSpeedUnit.Should().Be(windAfter);
        }

        [Theory,
        InlineData(25, 77),
        InlineData(0, 32),
        InlineData(5, 41)]
        public void ConvertsTempCorrectly(double celcius, double fahrenheit)
        {
            var weatherResult = TestData.GetTestWeatherResult(celcius, 25);
            weatherResult.ConvertTemperature(TemperatureUnit.DegreeFahrenheit);
            weatherResult.Temperature.Should().Be(fahrenheit);
        }

        [Theory,
        InlineData(10, 16),
        InlineData(50, 80)]
        public void ConvertsWindSpeedCorrectly(double mph, double kph)
        {
            var weatherResult = TestData.GetTestWeatherResult(10, mph);
            weatherResult.ConvertWindSpeed(SpeedUnit.KilometerPerHour);
            weatherResult.WindSpeed.Should().Be(kph);
        }
    }
}

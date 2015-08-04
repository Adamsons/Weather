using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UnitsNet;
using UnitsNet.Units;
using WeatherApp.Service.Extensions;
using WeatherApp.Service.ResultTypes;
using Xunit;

namespace WeatherApp.Tests.WeatherApp.Service.Extensions
{
    public class ExtensionsTests
    {
        [Fact]
        public void AverageWeatherResultsThrowsOnNull()
        {
            List<WeatherApiResult> list = null;

            Action action = () =>
            {
                list.AverageWeatherResults(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);
            };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AverageWeatherResultsReturnsNullOnAnEmptyList()
        {
            List<WeatherApiResult> emptyList = new List<WeatherApiResult>();
            var result = emptyList.AverageWeatherResults(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);
            result.Should().BeNull();
        }

        [Fact]
        public void AveragesWeatherResults()
        {
            WeatherApiResult celciusResult = TestData.GetTestWeatherResult(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);
            WeatherApiResult otherCelciusResult = TestData.GetTestWeatherResult(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);
            var weatherResults = new List<WeatherApiResult>() { celciusResult, otherCelciusResult };

            var averaged = weatherResults.AverageWeatherResults(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);

            averaged.TemperatureUnit.Should().Be(TemperatureUnit.DegreeCelsius);
            averaged.WindSpeedUnit.Should().Be(SpeedUnit.MilePerHour);
            averaged.Temperature.Should().Be(weatherResults.Average(o => o.Temperature));
            averaged.Temperature.Should().Be(weatherResults.Average(o => o.Temperature));
        }

        [Theory,
        InlineData(12, 20, 16),
        InlineData(4, 34, 19),
        InlineData(-8, 16, 4),]
        public void AveragesTempCorrectly(double minTemp, double maxTemp, double averageTemp)
        {
            WeatherApiResult minRange = TestData.GetTestWeatherResult(minTemp, 12);
            WeatherApiResult maxreRange = TestData.GetTestWeatherResult(maxTemp, 20);
            var weatherResults = new List<WeatherApiResult>() { minRange, maxreRange };

            var averaged = weatherResults.AverageWeatherResults(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);
            averaged.Temperature.Should().Be(averageTemp);
        }

        [Theory,
        InlineData(12, 20, 16),
        InlineData(4, 34, 19),
        InlineData(-2, 14, 6),]
        public void AveragesWindSpeedCorrectly(double min, double max, double average)
        {
            WeatherApiResult minRange = TestData.GetTestWeatherResult(min, 12);
            WeatherApiResult maxreRange = TestData.GetTestWeatherResult(max, 20);
            var weatherResults = new List<WeatherApiResult>() { minRange, maxreRange };

            var averaged = weatherResults.AverageWeatherResults(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);
            averaged.Temperature.Should().Be(average);
        }

        [Theory, 
        InlineData(TemperatureUnit.DegreeCelsius, TemperatureUnit.DegreeFahrenheit, SpeedUnit.KilometerPerHour, SpeedUnit.MilePerHour),
        InlineData(TemperatureUnit.DegreeFahrenheit, TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour, SpeedUnit.MilePerHour),
        InlineData(TemperatureUnit.DegreeCelsius, TemperatureUnit.DegreeCelsius, SpeedUnit.KilometerPerHour, SpeedUnit.KilometerPerHour)]
        public void ConvertsAllUnitsOfMeasurement(TemperatureUnit tempBefore, TemperatureUnit tempAfter, SpeedUnit speedBefore, SpeedUnit speedAfter)
        {
            WeatherApiResult resultA = TestData.GetTestWeatherResult(tempBefore, speedBefore);
            WeatherApiResult resultB = TestData.GetTestWeatherResult(tempBefore, speedBefore);
            WeatherApiResult resultc = TestData.GetTestWeatherResult(TemperatureUnit.DegreeCelsius, SpeedUnit.MilePerHour);
            WeatherApiResult resultd = TestData.GetTestWeatherResult(TemperatureUnit.DegreeFahrenheit, SpeedUnit.KilometerPerHour);

            var weatherResults = new List<WeatherApiResult>() { resultA, resultB, resultc, resultd };
            var averaged = weatherResults.AverageWeatherResults(tempAfter, speedAfter);

            weatherResults.TrueForAll(o => o.TemperatureUnit == tempAfter && o.WindSpeedUnit == speedAfter);
        }
    }
}

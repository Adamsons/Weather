using Newtonsoft.Json;
using UnitsNet.Units;

namespace WeatherApp.Service.ResultTypes
{
    public class BbcWeatherResult : WeatherApiResult
    {
        [JsonProperty("TemperatureCelsius")]
        public override double Temperature { get; set; }
        [JsonProperty("WindSpeedKph")]
        public override double WindSpeed { get; set; }

        public BbcWeatherResult()
        {
            TemperatureUnit = TemperatureUnit.DegreeCelsius;
            WindSpeedUnit = SpeedUnit.KilometerPerHour;
        }
    }
}

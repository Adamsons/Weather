using Newtonsoft.Json;
using UnitsNet.Units;

namespace WeatherApp.Service.ResultTypes
{
    public class AccuweatherResult : WeatherApiResult
    {
        [JsonProperty("TemperatureFahrenheit")]
        public override double Temperature { get; set; }
        [JsonProperty("WindSpeedMph")]
        public override double WindSpeed { get; set; }

        public AccuweatherResult()
        {
            TemperatureUnit = TemperatureUnit.DegreeFahrenheit;
            WindSpeedUnit = SpeedUnit.MilePerHour;
        }
    }
}

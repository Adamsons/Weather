using System;
using System.Net;
using UnitsNet;
using UnitsNet.Units;
using WeatherApp.Service.Extensions;

namespace WeatherApp.Service.ResultTypes
{
    public class WeatherApiResult
    {
        public virtual double Temperature { get; set; }
        public virtual double WindSpeed { get; set; }

        public TemperatureUnit TemperatureUnit { get; set; }
        public SpeedUnit WindSpeedUnit { get; set; }

        public string Location { get; set; }

        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        
        public string CreatedStamp { get; set; }

        public WeatherApiResult()
        {
            CreatedStamp = DateTime.Now.ToShortTimeString();
        }

        public void ConvertTemperature(TemperatureUnit unitOfMeasurement)
        {
            if (TemperatureUnit == unitOfMeasurement) return;

            Temperature = UnitsNet.Temperature.From(Temperature, TemperatureUnit).As(unitOfMeasurement).Round();
            TemperatureUnit = unitOfMeasurement;
        }

        public void ConvertWindSpeed(SpeedUnit unitOfMeasurement)
        {
            if (WindSpeedUnit == unitOfMeasurement) return;

            WindSpeed = Speed.From(WindSpeed, WindSpeedUnit).As(unitOfMeasurement).Round();
            WindSpeedUnit = unitOfMeasurement;
        }

        public void Round()
        {
            WindSpeed = WindSpeed.Round();
            Temperature = Temperature.Round();
        }
    }
}

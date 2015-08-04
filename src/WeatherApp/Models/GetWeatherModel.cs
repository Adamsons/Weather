using System.ComponentModel.DataAnnotations;
using UnitsNet.Units;

namespace WeatherApp.Models
{
    public class GetWeatherModel
    {
        [Required]
        [StringLength(50)]
        public string Location { get; set; }
        [StringLength(10)]
        [Required]
        public string Temperature { get; set; }
        [StringLength(10)]
        [Required]
        public string WindSpeed { get; set; }
    }
}
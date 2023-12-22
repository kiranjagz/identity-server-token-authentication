using System.ComponentModel.DataAnnotations;

namespace Test.Web.Api.Models
{
    public class WeatherForecast
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }

        [Range(0, 99)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Required]
        public string? Summary { get; set; }
    }
}
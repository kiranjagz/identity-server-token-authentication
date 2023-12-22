using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Web.Api.DataContext;
using Test.Web.Api.Models;

namespace Test.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var values = Enumerable.Range(1, 50).Select(index => new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            _databaseContext.WeatherForecasts.AddRange(values);
            _databaseContext.SaveChanges();

            _logger.LogInformation("Completed GET");

            return values;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var value = _databaseContext.WeatherForecasts.SingleOrDefault(x => x.Id == id);

            _logger.LogInformation("Completed GET BY ID");

            return Ok(value);
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast value)
        {
            if (ModelState.IsValid)
            {
                _databaseContext.WeatherForecasts.Add(value);
                _databaseContext.SaveChanges(true);
            }

            _logger.LogInformation("Completed POST");

            return Created(Request.Path.Value.ToLower(), value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        { 
            var result = _databaseContext.WeatherForecasts.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            _databaseContext.WeatherForecasts.Remove(result);
            _databaseContext.SaveChanges();

            return Ok();
        }
    }
}
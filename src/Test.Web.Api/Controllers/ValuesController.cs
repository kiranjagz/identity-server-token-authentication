using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Web.Api.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LogFilter))]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public ValuesController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Entered the get method that returns a enumerable");

            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public string Get(int id)
        {
            if (id <= 1)
            {
                _logger.LogError("The Id used is invalid", id);
                return id.ToString();
            }

            _logger.Log(LogLevel.Information, $"Entered the get method a string with id: {id}");

            return id.ToString();
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Post([FromBody] string value)
        {
        }
    }
}

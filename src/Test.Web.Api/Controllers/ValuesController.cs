using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public ValuesController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        //[Authorize]
        public IActionResult Get()
        {
            _logger.LogInformation("Entered the get method that returns a enumerable");

            return Ok(new
            {
                Value = "Value"
            });
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Get(int id)
        {
            if (id <= 1)
            {
                _logger.LogError($"The Id used is invalid: {id}");
                return BadRequest();
            }

            _logger.LogInformation($"Entered the get method a string with id: {id}");

            return Ok(new
            {
                Id = id
            });
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] string value)
        {
            return Ok();
        }
    }
}

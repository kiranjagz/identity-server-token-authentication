using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Test.Web.Api.Services.Security;
using Test.Web.Api.Services.Security.Models;

namespace Test.Web.Api.Controllers.Security
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegiserUser userParam)
        {
            var token = await _userService.RegisterAsync(userParam);

            if (token == null || string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Error" });

            var tokenResponse = new
            {
                token
            };

            return Ok(tokenResponse);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUser userParam)
        {
            var token = await _userService.LoginAsync(userParam);

            if (token == null || string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Error" });

            var tokenResponse = new
            {
                token
            };

            return Ok(tokenResponse);
        }
    }
}

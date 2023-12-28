using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Test.Web.Api.Services.Security;
using Test.Web.Api.Services.Security.Models;
using Test.Web.Api.Models;

namespace Test.Web.Api.Controllers.Security
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegiserUser userParam)
        {
            try
            {
                var token = await _userService.RegisterAsync(userParam);

                // TODO: Poor logic for checking the result
                if (token == null || string.IsNullOrEmpty(token))
                    return Unauthorized();

                var response = new
                {
                    token
                };

                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUser userParam)
        {
            try
            {
                var token = await _userService.LoginAsync(userParam);

                if (token == null || string.IsNullOrEmpty(token))
                    return Unauthorized();

                var response = new
                {
                    token
                };

                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }
    }
}

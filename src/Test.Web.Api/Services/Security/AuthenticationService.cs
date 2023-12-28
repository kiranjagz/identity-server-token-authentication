using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Web.Api.Models;
using Test.Web.Api.Services.Security.Models;
using static Test.Web.Api.Extensions.AuthenticationService;

namespace Test.Web.Api.Services.Security
{
    public interface IUserService
    {
        Task<string?> RegisterAsync(RegiserUser userRequest);

        Task<string?> LoginAsync(LoginUser userRequest);
    }

    public class AuthenticationService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManager;
        }

        public async Task<string?> RegisterAsync(RegiserUser userRequest)
        {
            // HACK: Need to return a better model with error messages for register
            IdentityUser user = new()
            {
                Email = userRequest.Username,
                UserName = userRequest.Username,
                PhoneNumber = userRequest.PhoneNumber
            };

            var identityResult = await _userManager.CreateAsync(user, userRequest.Password);

            if (identityResult.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(userRequest.Role))
                {
                    var createResult = await _roleManager.CreateAsync(new IdentityRole(userRequest.Role));
                    if (!createResult.Succeeded) { return null; }
                }

                var userResponse = await _userManager.FindByEmailAsync(user.Email);

                if (userResponse == null) { return null; }

                var roleResult = await _userManager.AddToRoleAsync(user, userRequest.Role);

                var claims = new List<Claim>();

                if (userRequest.Claims.Any())
                {
                    foreach (var claim in userRequest.Claims)
                    {
                        claims.Add(new Claim(claim.Key, claim.Value));
                    }

                    await _userManager.AddClaimsAsync(user, claims);
                }

                if (roleResult.Succeeded)
                {
                    var token = await GenerateJWTTokenAsync(user);

                    return (string)token;
                }
            }

            return null;
        }

        public async Task<string?> LoginAsync(LoginUser loginRequest)
        {
            // HACK: Need to return a better model with error messages for login
            var  user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);

                if (result == PasswordVerificationResult.Failed) { return null; }

                var token = await GenerateJWTTokenAsync(user);

                return (string)token;
            }

            return null;
        }

        private async Task<string> GenerateJWTTokenAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
            };

            var roleResult = await _userManager.GetRolesAsync(user);

            foreach (var role in roleResult)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsResult = await _userManager.GetClaimsAsync(user);

            foreach(var claim in claimsResult)
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                IssuedAt = DateTime.UtcNow
            };

            var createToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createToken);
        }
    }
}
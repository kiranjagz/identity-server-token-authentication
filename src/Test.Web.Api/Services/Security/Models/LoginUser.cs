using System.ComponentModel.DataAnnotations;

namespace Test.Web.Api.Services.Security.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
    }
}

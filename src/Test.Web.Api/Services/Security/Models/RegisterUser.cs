using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Web.Api.Services.Security.Models
{
    public class RegiserUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username is required. Username is an email address.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
        public Dictionary<string, string>? Claims { get; set; }
    }

    // TODO: Use role type instead of string value
    public class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Web.Api.Services.Security.Models
{
    public class RegiserUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }

    public class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
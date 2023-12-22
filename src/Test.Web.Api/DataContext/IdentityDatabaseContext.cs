using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Test.Web.Api.DataContext
{
    public class IdentityDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : base(options)
        {

        }
    }
}

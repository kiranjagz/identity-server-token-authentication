using Microsoft.EntityFrameworkCore;
using Test.Web.Api.Models;

namespace Test.Web.Api.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer(@"Server=LSBSA-7MYYJS3\MSSQLSERVER01;Database=Test;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //}
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set;}
    }
}

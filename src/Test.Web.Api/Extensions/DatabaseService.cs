using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Test.Web.Api.DataContext;
using Test.Web.Api.Models;

namespace Test.Web.Api.Extensions
{
    public static class DatabaseService
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<DatabaseSettings>()
                .BindConfiguration(nameof(DatabaseSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services
                .AddDbContext<DatabaseContext>((service, options) =>
                {
                    var databaseSettings = service.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                    options.UseSqlServer(databaseSettings.ConnectionString);
                });

            return services;
        }

        public class DatabaseSettings
        {
            public string? DbProvider { get; set; }
            public string? ConnectionString { get; set; }
        }
    }
}

using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Test.Web.Api.Extensions
{
    public static class ConfigurationsService
    {
        public static WebApplicationBuilder AddConfigurations(this  WebApplicationBuilder builder)
        {
            const string configsDirectory = "Configurations";
            var env = builder.Environment;

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configsDirectory}/database.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configsDirectory}/jwt.json", optional : false, reloadOnChange: true)
                    .AddEnvironmentVariables();

            return builder;
        }
    }
}

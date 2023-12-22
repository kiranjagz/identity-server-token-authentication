using Json.Converter.To.Xml.Services;
using Test.Web.Api.Filters;
using Test.Web.Api.Services.Security;

namespace Test.Web.Api.Extensions
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUserService, Services.Security.AuthenticationService>();
            services.AddTransient<LogFilter>();
            services.AddTransient<IConvertJsonToXML, ConvertJsonToXML>();

            return services;
        }
    }
}

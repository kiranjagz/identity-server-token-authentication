using Json.Converter.To.Xml.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Test.Web.Api.DataContext;
using Test.Web.Api.Filters;
using Test.Web.Api.Models;
using static Test.Web.Api.Extensions.DatabaseService;

namespace Test.Web.Api.Extensions
{
    public static class AuthenticationService
    {
        // TODO: Add this to the config
        private static string ConnectionStringTemp => "Server=localhost;Database=Testing_World;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<JwtSettings>()
                .BindConfiguration(nameof(JwtSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(ConnectionStringTemp));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //! Change your security Policy here to suits your policy
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<IdentityDatabaseContext>()
            .AddDefaultTokenProviders();

            var jwtSettingsSection = config.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                };
            });

            return services;
        }

        public class JwtSettings
        {
            public string? Key { get; set; }
            public string? Issuer { get; set; }
            public string? Audience { get; set; }
            public string? Subject { get; set; }
            public string? Metadata { get; set; }
        }
    }
}

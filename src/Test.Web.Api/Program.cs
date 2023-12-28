using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Test.Web.Api.DataContext;
using Test.Web.Api.Models;
using Serilog;
using Serilog.Events;
using Serilog.Core;
using Microsoft.Extensions.Options;
using Test.Web.Api.Filters;
using Test.Web.Api.Extensions;

namespace Test.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddConfigurations();

            // configure the logging using console and serilog
            var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Add services to the container
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Create and place in its own class for explicit services
            builder.Services
                .AddDatabaseServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddAuthenticationServices(builder.Configuration)
                .AddHealthChecks();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<LogFilter>();
            })
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "SB Api",
                    Description = "Does important stuff",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Name",
                        Email = "Email",
                        Url = new Uri("http://localhost:8080/")
                    },
                    TermsOfService = new Uri("http://localhost:8080/"),
                    License = new Microsoft.OpenApi.Models.OpenApiLicense { Name = "License", Url = new Uri("http://localhost:8080/") }
                });

                c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
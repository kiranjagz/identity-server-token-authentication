using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Security.Authentication.ExtendedProtection;

namespace Http.Console.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // register the services upfront
            var services = new ServiceCollection();

            services.AddHttpClient<IJsonPlaceHolder, JsonPlaceHolder>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            });

            //services.AddScoped<IJsonPlaceHolder, JsonPlaceHolder>();

            // build the servics
            var serviceProvider = services.BuildServiceProvider();

            // resolve the service
            var myService = serviceProvider.GetRequiredService<IJsonPlaceHolder>();

            // use service
            var data = await myService.Get();

            await System.Console.Out.WriteLineAsync(data);


        }
    }
}


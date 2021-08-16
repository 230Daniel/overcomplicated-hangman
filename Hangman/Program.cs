using System;
using System.Threading.Tasks;
using Hangman.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hangman
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(builder => builder.AddCommandLine(args))
                .ConfigureServices(ConfigureServices)
                .Build();

            try
            {
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Application crashed");
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<HangmanService>();
            services.AddSingleton<IGameService, GameService>();
            services.AddTransient<IWordService, WordService>();

            services.AddSingleton<Random>();
        }
    }
}

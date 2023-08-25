using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace LifeHabitTrackerConsole
{ 

    class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Приложение запущено.");

            IServiceCollection? services = GetServiceCollection();
            ServiceProvider? serviceProvider = services.BuildServiceProvider();
            IBot? botService = serviceProvider.GetService<IBot>();
            await botService.Launch();
            
            Console.ReadLine();
        }

        private static IServiceCollection GetServiceCollection() =>
                new ServiceCollection()
                    .AddSingleton<IBot,TelegramBot>();

    }
}


using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;

namespace LifeHabitTrackerConsole
{ 

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Приложение запущено.");

            var services = GetServiceCollection();
            var serviceProvider = services.BuildServiceProvider();
            var botService = serviceProvider.GetService<IBot>();
            await botService.Launch();
            
            Console.ReadLine();
        }

        private static IServiceCollection GetServiceCollection() =>
                new ServiceCollection()
                    .AddSingleton<IBot,TelegramBot>();

    }
}


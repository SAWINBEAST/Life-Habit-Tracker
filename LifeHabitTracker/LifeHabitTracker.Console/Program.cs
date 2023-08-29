using Microsoft.Extensions.DependencyInjection;

using LifeHabitTrackerConsole;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Приложение запущено.");

            var services = GetServiceCollection();
            using var serviceProvider = services.BuildServiceProvider();
            var botService = serviceProvider.GetService<IBot>();
            await botService.LaunchAsync();

            
            Console.ReadLine();
        }

        private static IServiceCollection GetServiceCollection() =>
                new ServiceCollection()
                    .AddSingleton<IBot,TelegramBot>();

    }



using Microsoft.Extensions.DependencyInjection;

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

            //Может так сделать ? Но будет менее читабельно
            //await GetServiceCollection().BuildServiceProvider().GetService<IBot>().Launch();

            Console.ReadLine();
        }

        private static IServiceCollection GetServiceCollection() =>
                new ServiceCollection()
                    .AddSingleton<IBot,TelegramBot>();

    }
}


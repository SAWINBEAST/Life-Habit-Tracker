using Microsoft.Extensions.DependencyInjection;

using LifeHabitTrackerConsole;
using LifeHabitTracker.BusinessLogicLayer;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Приложение запущено.");

            Caretaker taker = new Caretaker();

            var services = GetServiceCollection();
            using var serviceProvider = services.BuildServiceProvider();
            var botService = serviceProvider.GetService<IBot>();
            await botService.LaunchAsync();

            
            Console.ReadLine();
        }

    private static IServiceCollection GetServiceCollection() =>
            new ServiceCollection()
                .AddSingleton<IBot, TelegramBot>()
/*                .AddTransient<IHabitService, HabitService>()
*//*                .AddTransient<IMemento, Memento>()
*/                .AddTransient<ICaretaker, Caretaker>()
                .AddTransient<IOriginator, Originator>();

    }



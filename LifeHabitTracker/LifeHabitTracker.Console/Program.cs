using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTrackerConsole;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine($"Приложение запущено.");

        /// <summary>
        /// Создание провайдера сервисов
        /// <summary>
        var services = GetServiceCollection();
        using var serviceProvider = services.BuildServiceProvider();

        var botService = serviceProvider.GetService<IBot>();
        await botService.LaunchAsync();

        Console.ReadLine();
    }

    private static IServiceCollection GetServiceCollection() =>
            new ServiceCollection()
                .AddSingleton<IBot, TelegramBot>()
                .AddTransient<IHabitContextCaretaker, ContextsCaretaker>()
                .AddTransient<IHabitService, HabitService>()
                .AddTransient<NameState>()
                .AddTransient<TypeState>()
                .AddTransient<DescState>()
                .AddTransient<DateState>();

}
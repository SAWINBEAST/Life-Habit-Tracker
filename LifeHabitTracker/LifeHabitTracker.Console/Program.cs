using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Impls;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTrackerConsole;
using Microsoft.Extensions.DependencyInjection;

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

    /// <summary>
    /// Получение нужного функционала(сервисов) для работы бота слабосвязанным путём
    /// </summary>
    /// <returns>Коллекция сервисов, осуществляющие работу трекера</returns>
    private static IServiceCollection GetServiceCollection() =>
            new ServiceCollection()
                .AddSingleton<IBot, TelegramBot>()
                .AddTransient<IHabitContextCaretaker, ContextsCaretaker>()
                .AddTransient<IHabitService, HabitService>()
                .AddTransient<NameState>()
                .AddTransient<TypeState>()
                .AddTransient<DescState>()
                .AddTransient<DateState>()
                .AddTransient<IDataManage, DataManage>();

}
using LifeHabitTracker.BusinessLogicLayer;
using LifeHabitTracker.DataAccessLayer;
using LifeHabitTrackerConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    public static IConfiguration Configuration => new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

    static async Task Main(string[] args)
    {
        Console.WriteLine($"Приложение запущено.");

        var services = GetServiceCollection();

        using var serviceProvider = services.BuildServiceProvider();
       
        var botService = serviceProvider.GetService<ITelegramBot>();
        await botService.LaunchAsync();

        Console.ReadLine();
    }

    /// <summary>
    /// Получение нужного функционала(сервисов) для работы бота слабосвязанным путём
    /// </summary>
    /// <returns>Коллекция сервисов, осуществляющие работу трекера</returns>
    private static IServiceCollection GetServiceCollection() =>
            new ServiceCollection()
                .AddSingleton<ITelegramBot, TelegramBot>()
                .AddBusinessLogicServices()
                .AddDataAccessServices(Configuration);

}
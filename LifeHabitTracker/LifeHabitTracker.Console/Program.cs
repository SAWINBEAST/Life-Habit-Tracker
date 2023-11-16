using LifeHabitTracker.BusinessLogicLayer;
using LifeHabitTracker.DataAccessLayer;
using LifeHabitTracker.Entities;
using LifeHabitTrackerConsole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

class Program
{
    /// <summary>
    /// Строка доступа к БД
    /// </summary>
    private static DataBaseConnect _dBConfig;

    static async Task Main(string[] args)
    {
        Console.WriteLine($"Приложение запущено.");

        //TODO:Вывести в отдельный метод
        var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

        var section = config.GetSection("DataBaseConnect");
        _dBConfig = section.Get<DataBaseConnect>();


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
                .AddDataAccessServices()
                .AddSingleton(_dBConfig);

}
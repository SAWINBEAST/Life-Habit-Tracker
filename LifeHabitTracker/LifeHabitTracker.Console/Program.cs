using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.BusinessLogicLayer;
using LifeHabitTracker.DataAccessLayer.Impls;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTrackerConsole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


class Program
{
    private static DataBaseConnect _dBConfig;

    static async Task Main(string[] args)
    {
        Console.WriteLine($"Приложение запущено.");

        var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

        var section = config.GetSection("DataBaseConnect");
        _dBConfig = section.Get<DataBaseConnect>();


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
                .AddTransient<IHabitsTableRepository, HabitsTableRepository>()
                .AddTransient<IDaysTableRepository, DaysTableRepository>()
                .AddTransient<ITimesTableRepository, TimesTableRepository>()
                .AddSingleton(_dBConfig);


}
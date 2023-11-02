using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Impls;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTrackerConsole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using LifeHabitTracker.DataAccessLayer.Impls.Repositories;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Impls;
using LifeHabitTracker.BusinessLogicLayer.Entities;

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
                .AddTransient<IContextCaretaker, ContextsCaretaker>()
                .AddTransient<IHabitService, HabitService>()
                .AddTransient<NameState>()
                .AddTransient<TypeState>()
                .AddTransient<DescState>()
                .AddTransient<DateState>()
                .AddTransient<IHabitsTableRepository, HabitsTableRepository>()
                .AddTransient<IDaysTableRepository, DaysTableRepository>()
                .AddTransient<ITimesTableRepository, TimesTableRepository>()
                .AddTransient<IInsertHabitService, InsertHabitService>()
                .AddSingleton(_dBConfig);


}
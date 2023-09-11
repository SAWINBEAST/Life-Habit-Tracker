using Microsoft.Extensions.DependencyInjection;
using LifeHabitTrackerConsole;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Impls;

class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Приложение запущено.");

            //Создание провайдера сервисов
            var services = GetServiceCollection();
            using var serviceProvider = services.BuildServiceProvider();

            //Запись бота
            var botService = serviceProvider.GetService<IBot>();
            await botService.LaunchAsync();

            Console.ReadLine();
        }

    private static IServiceCollection GetServiceCollection() =>
            new ServiceCollection()
                .AddSingleton<IBot, TelegramBot>()
                .AddScoped<IHabitService, HabitService>()

                .AddTransient<ICaretaker, Caretaker>()
                .AddTransient<IOriginator, Originator>()

                .AddScoped<IReciever, Reciever>()
                .AddTransient<INameHandler, NameHandler>()
                .AddTransient<ITypeHandler, TypeHandler>()
                .AddTransient<IDescHandler, DescriptionHandler>()
                .AddTransient<IDateHandler, DateHandler>();
       
    }



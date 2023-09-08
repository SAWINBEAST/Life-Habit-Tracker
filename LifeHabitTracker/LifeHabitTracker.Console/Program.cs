using Microsoft.Extensions.DependencyInjection;

using LifeHabitTrackerConsole;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Impls;

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
        //мне кажется какие-то нужно сделать через Scoped //пробовал разные зависимости сделать через скоупд, но лучше не стало
                .AddSingleton<IBot, TelegramBot>()
                .AddTransient<IHabitService, HabitService>()

                .AddTransient<ICaretaker, Caretaker>()
                .AddTransient<IOriginator, Originator>()

                .AddTransient<IReciever, Reciever>()
                .AddTransient<IDataHandler, NameHandler>()
                .AddTransient<IDataHandler, TypeHandler>()
                .AddTransient<IDataHandler, DescriptionHandler>()
                .AddTransient<IDataHandler, DateHandler>();
       


    }



using Microsoft.Extensions.DependencyInjection;
using LifeHabitTrackerConsole;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Impls;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IHabit;
using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Impls.General;

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

                .AddTransient<ICaretaker, Caretaker>()
                .AddTransient<IOriginator, Originator>()

                .AddScoped<IContextHabitCreation, ContextHabitCreation>()
                .AddScoped<IState, InitialState>();

                       
    }



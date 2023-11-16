using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using LifeHabitTracker.BusinessLogicLayer.Impls;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Класс расширений (Расширенных возможностей для внешних проектов) уровня BusinessLogic
    /// </summary>
    public static class BusinessLogicExtensions
    {
        /// <summary>
        /// Предоставление зависимостей уровня проекта BusinessLogic для внешних внедрений
        /// </summary>
        /// <param name="services"> Внешний объект ServiceCollection, к которому применяется добавление зависимостей </param>
        /// <returns> Коллекция предоставляемых сервисов </returns>
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
            => services
                .AddTransient<IContextCaretaker, ContextsCaretaker>()
                .AddTransient<IHabitService, HabitService>()
                .AddTransient<NameState>()
                .AddTransient<TypeState>()
                .AddTransient<DescState>()
                .AddTransient<DateState>();

    }
}

using LifeHabitTracker.DataAccessLayer.Impls.Repositories;
using LifeHabitTracker.DataAccessLayer.Impls;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LifeHabitTracker.DataAccessLayer
{
    /// <summary>
    /// Класс расширений (Расширенных возможностей для внешних проектов) уровня DataAccess
    /// </summary>
    public static class DataAccessExtensions
    {
        /// <summary>
        /// Предоставление зависимостей уровня проекта DataAccess для внешних внедрений
        /// </summary>
        /// <param name="services"> Внешний объект ServiceCollection, к которому применяется добавление зависимостей </param>
        /// <returns> Коллекция предоставляемых сервисов </returns>
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
            => services
                .AddTransient<IHabitsRepository, HabitsRepository>()
                .AddTransient<IDaysRepository, DaysRepository>()
                .AddTransient<ITimesRepository, TimesRepository>()
                .AddTransient<IInsertHabitService, InsertHabitService>();
    }
}

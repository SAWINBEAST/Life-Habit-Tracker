using LifeHabitTracker.DataAccessLayer.Impls.Repositories;
using LifeHabitTracker.DataAccessLayer.Impls;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
        /// <param name="configuration"></param>
        /// <returns> Коллекция предоставляемых сервисов </returns>
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<DataBaseConnect>(configuration.GetSection(nameof(DataBaseConnect)))
                .AddTransient<IHabitsRepository, HabitsRepository>()
                .AddTransient<IDaysRepository, DaysRepository>()
                .AddTransient<ITimesRepository, TimesRepository>()
                .AddTransient<IDBHabitProvider, DBHabitProvider>();
    }
}

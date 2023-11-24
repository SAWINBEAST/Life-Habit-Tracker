using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;

namespace LifeHabitTracker.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс добавления всех данных о привычке
    /// </summary>
    public interface IInsertHabitService
    {
        /// <summary>
        /// Добавление всех данных о привычке
        /// </summary>
        /// <param name="preparedHabits">Подготовленные основные данные о привычке</param>
        /// <param name="preparedDays">Подготовленные данные о днях напоминания</param>
        /// <param name="preparedTimes">Подготовленные данные о времени напоминания</param>
        /// <returns>Результат выполнения транзакции записи</returns>
        public Task<bool> InsertHabitAsync(DbHabits preparedHabits, DbDays preparedDays, DbTimes preparedTimes);
    }
}
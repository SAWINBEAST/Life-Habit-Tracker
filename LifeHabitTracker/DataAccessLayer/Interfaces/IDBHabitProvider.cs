using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;

namespace LifeHabitTracker.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс добавления всех данных о привычке
    /// </summary>
    public interface IDBHabitProvider
    {
        /// <summary>
        /// Добавление всех данных о привычке
        /// </summary>
        /// <param name="preparedHabits">Подготовленные основные данные о привычке</param>
        /// <param name="preparedDays">Подготовленные данные о днях напоминания</param>
        /// <param name="preparedTimes">Подготовленные данные о времени напоминания</param>
        /// <returns>Результат выполнения транзакции записи</returns>
        public Task<bool> InsertHabitAsync(DbHabits preparedHabits, DbDays preparedDays, DbTimes preparedTimes);

        /// <summary>
        /// Выгрузка всех привычек пользователя
        /// </summary>
        /// <param name="chatId">Идентификатор пользователя</param>
        /// <returns>Массив привычек и информацией о них</returns>
        public Task<IReadOnlyCollection<DbHabits>> SelectHabitsInfoAsync(long chatId);

        /// <summary>
        /// Выгрузка определённой привычки пользователя
        /// </summary>
        /// <param name="chatId">Идентификатор пользователя</param>
        /// <param name="requestedHabit">Название запрашеваемой привычки</param>
        /// <returns>Данные о привычки вида Базы Данных</returns>
        public Task<(DbHabits,DbDays,DbTimes)> SelectCertainHabitInfoAsync(long chatId, string requestedHabit);
    }
}
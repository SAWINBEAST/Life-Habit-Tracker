using LifeHabitTracker.Entities.PreparedData;
using Microsoft.Data.Sqlite;

namespace LifeHabitTracker.DataAccessLayer.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс взаимодействия с таблицей times
    /// </summary>
    public interface ITimesRepository
    {
        /// <summary>
        /// Записать время напоминания о привычке
        /// </summary>
        /// <param name="timesTableData">Подготовленные для записи данные</param>
        /// <param name="habitId">ID привычки, к которой относится информация из таблицы days. (Внешний ключ)</param>
        /// <param name="connection">Соединение с БД в текущей транзакции</param>
        /// <param name="transaction">Объект текущей транзакции</param>
        /// <returns>Оценка результата выполнения записи</returns>
        public Task<bool> InsertIntoTimesTableAsync(DbTimes timesTableData, long habitId, SqliteConnection connection, SqliteTransaction transaction);
    }
    ///         


}
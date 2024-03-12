using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using Microsoft.Data.Sqlite;


namespace LifeHabitTracker.DataAccessLayer.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс взаимодействия с таблицей days
    /// </summary>
    public interface IDaysRepository
    {
        /// <summary>
        /// Записать дни напоминания о привычке
        /// </summary>
        /// <param name="daysTableData">Подготовленные данные о днях напоминания</param>
        /// <param name="habitId">ID привычки, к которой относится информация из таблицы days. (Внешний ключ)</param>
        /// <param name="connection">Соединение с БД в текущей транзакции</param>
        /// <param name="transaction">Объект текущей транзакции</param>
        /// <returns>Оценка результата выполнения записи</returns>
        public Task<bool> InsertIntoDaysTableAsync(DbDays daysTableData, long habitId, SqliteConnection connection, SqliteTransaction transaction);

        /// <summary>
        /// Выгрузить Дни напоминания определённой привычки
        /// </summary>
        /// <param name="habitId">ID привычки, к которой относится информация из таблицы days. (Внешний ключ)</param>
        /// <param name="connection">Соединение с БД</param>
        /// <returns>Объект дней напоминания</returns>
        public Task<DbDays> SelectFromDaysTableAsync(int habitId, SqliteConnection connection);
    }
}  
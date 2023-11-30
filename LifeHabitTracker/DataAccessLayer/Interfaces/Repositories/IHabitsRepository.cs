using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using Microsoft.Data.Sqlite;

namespace LifeHabitTracker.DataAccessLayer.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс взаимодействия с таблицей habits
    /// </summary>
    public interface IHabitsRepository
    {
        /// <summary>
        /// Записать основные данные о привычке
        /// </summary>
        /// <param name="habitsTableData">Подготовленные для записи данные</param>
        /// <param name="connection">Соединение с БД в текущей транзакции</param>
        /// <param name="transaction">Объект текущей транзакции</param>
        /// <returns>Идентификатор привычки, для дальнейго использования</returns>
        public Task<long> InsertIntoHabitsTableAsync(DbHabits habitsTableData, SqliteConnection connection, SqliteTransaction transaction);

        /// <summary>
        /// Выгрузить все привычки данного пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Привычки и информация о них</returns>
        public Task<IReadOnlyCollection<DbHabits>> SelectAllUserHabits (long chatId, SqliteConnection connection);
    }
}

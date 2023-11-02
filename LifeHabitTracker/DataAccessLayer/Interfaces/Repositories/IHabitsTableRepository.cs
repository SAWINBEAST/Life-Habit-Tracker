using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LifeHabitTracker.DataAccessLayer.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс взаимодействия с таблицей habits
    /// </summary>
    public interface IHabitsTableRepository
    {
        /// <summary>
        /// Записать основные данные о привычке
        /// </summary>
        /// <param name="habitsTableData">Подготовленные для записи данные</param>
        /// <param name="connection">Соединение с БД в текущей транзакции</param>
        /// <param name="transaction">Объект текущей транзакции</param>
        /// <returns>Идентификатор привычки, для дальнейго использования</returns>
        public Task<long> InsertIntoHabitsTableAsync(PreparedHabitsTableData habitsTableData, SqliteConnection connection, SqliteTransaction transaction);
    }

}

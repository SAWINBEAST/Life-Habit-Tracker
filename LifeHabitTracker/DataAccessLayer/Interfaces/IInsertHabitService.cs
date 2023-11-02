using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="dbName">Строка подключения к БД</param>
        /// <returns>Результат выполнения транзакции записи</returns>
        public Task<bool> InsertHabitAsync(PreparedHabitsTableData preparedHabits, PreparedDaysTableData preparedDays, PreparedTimesTableData preparedTimes, string dbName);


    }
}

using LifeHabitTracker.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс взаимодействия с таблицей times
    /// </summary>
    public interface ITimesTableRepository
    {
        /// <summary>
        /// Записать время напоминания о привычке
        /// </summary>
        /// <param name="timesTableData">Подготовленные для записи данные</param>
        /// <param name="dbName">Строка подключения к БД</param>
        /// <returns>Оценка результата выполнения записи</returns>
        public Task<bool> InsertIntoTimesTableAsync(PreparedTimesTableData timesTableData, string dbName, long habitId);
    }

}         
using LifeHabitTracker.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс взаимодействия с таблицей days
    /// </summary>
    public interface IDaysTableRepository
    {
        /// <summary>
        /// Записать дни напоминания о привычке
        /// </summary>
        /// <param name="daysTableData">Подготовленные для записи данные</param>
        /// <param name="dbName">Строка подключения к БД</param>
        /// <returns>Оценка результата выполнения записи</returns>
        public Task<bool> InsertIntoDaysTableAsync(PreparedDaysTableData daysTableData, string dbName, long habitId);
    }

}

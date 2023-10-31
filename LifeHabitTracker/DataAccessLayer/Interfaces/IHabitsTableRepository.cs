using LifeHabitTracker.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Interfaces
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
        /// <param name="dbName">Строка подключения к БД</param>
        /// <returns>Оценка результата выполнения записи</returns>
        public Task<long> InsertIntoHabitsTableAsync(PreparedHabitsTableData habitsTableData, string dbName);
        }
    
}

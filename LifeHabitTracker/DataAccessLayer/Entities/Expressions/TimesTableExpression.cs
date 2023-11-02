using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Entities.Expressions
{
    /// <summary>
    /// Хранитель шаблонов SQL-выражений для взаимодействия с таблицей times
    /// </summary>
    static class TimesTableExpression
    {
        /// <summary>
        /// Ввод данных о времени
        /// </summary>
        public const string InsertTime = "INSERT INTO times (habit_id, time) " +
                                      "VALUES (@habit_id, @time); ";
    }
}

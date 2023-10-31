using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Entities
{
    /// <summary>
    /// Хранитель шаблонов SQL-выражений для взаимодействия с таблицей days
    /// </summary>
    static class DaysTableExpression
    {
        /// <summary>
        /// Ввод данных о днях
        /// </summary>
        public const string InsertDays = "INSERT INTO days (habit_id, on_monday, on_tuesday, on_wednesday, on_thursday, on_friday, on_saturday, on_sunday) " +
                                                "VALUES (@habit_id, @monday, @tuesday, @wednesday, @thursday, @friday ,@saturday ,@sunday);";
    }
}

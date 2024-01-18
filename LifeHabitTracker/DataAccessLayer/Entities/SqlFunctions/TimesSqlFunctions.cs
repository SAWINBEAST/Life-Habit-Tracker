﻿
namespace LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions
{
    /// <summary>
    /// Хранитель шаблонов SQL-выражений для взаимодействия с таблицей times
    /// </summary>
    internal static class TimesSqlFunctions
    {
        /// <summary>
        /// Ввод данных о времени
        /// </summary>
        public const string InsertTime = @"INSERT INTO times (habit_id, time)
                                           VALUES (@habit_id, @time); ";

        /// <summary>
        /// Вывод времени напоминания о привычке
        /// </summary>
        public const string SelectTimes = @"SELECT time
                                            WHERE habit_id = @habit_id";
    }
}

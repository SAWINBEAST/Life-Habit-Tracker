
namespace LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions
{
    /// <summary>
    /// Хранитель шаблонов SQL-выражений для взаимодействия с таблицей days
    /// </summary>
    internal static class DaysSqlFunctions
    {
        /// <summary>
        /// Ввод данных о днях
        /// </summary>
        public const string InsertDays = @"INSERT INTO days (habit_id, on_monday, on_tuesday, on_wednesday, on_thursday, on_friday, on_saturday, on_sunday)  
                                         VALUES (@habit_id, @monday, @tuesday, @wednesday, @thursday, @friday ,@saturday ,@sunday);";

        /// <summary>
        /// Вывод дней напоминания о привычке
        /// </summary>
        public const string SelectDays = @"SELECT on_monday, on_tuesday, on_wednesday, on_thursday, on_friday, on_saturday, on_sunday
                                           FROM days
                                           WHERE habit_id = @habit_id";
    }
}

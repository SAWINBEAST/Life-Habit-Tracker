
namespace LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions
{
    /// <summary>
    /// Хранитель шаблонов SQL-выражений для взаимодействия с таблицей habits
    /// </summary>
    internal static class HabitsSqlFunctions
    {
        /// <summary>
        /// Ввод основной информации о привычке
        /// </summary>
        public const string InsertAllFields = @"INSERT INTO habits (name, desc, chat_id, is_good)
                                                VALUES (@name, @desc, @chatid, @isgood); 
                                                SELECT last_insert_rowid();";

        /// <summary>
        /// Вывод всех привычек (их названия, типы, описания) пользователя
        /// </summary>
        public const string SelectAllHabits = @"SELECT name, desc, is_good 
                                                FROM habits
                                                WHERE chat_id = @chatid";
    }
}


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
        public const string InsertAllFields = "INSERT INTO habits (name, desc, chat_id, is_good) " +
                                                  "VALUES (@name, @desc, @chatid, @isgood); " +
                                                  "SELECT last_insert_rowid();";

    }
}

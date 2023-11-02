using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Entities.Expressions
{
    /// <summary>
    /// Хранитель шаблонов SQL-выражений для взаимодействия с таблицей habits
    /// </summary>
    static class HabitsTableExpression
    {
        /// <summary>
        /// Ввод основной информации о привычке
        /// </summary>
        public const string InsertAllFields = "INSERT INTO habits (name, desc, chat_id, is_good) " +
                                                  "VALUES (@name, @desc, @chatid, @isgood); " +
                                                  "SELECT last_insert_rowid();";

    }
}

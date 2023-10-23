using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Entities
{
    /// <summary>
    /// Хранитель шаблонов SQL-выражений 
    /// </summary>
    public class Expression
    {
        public const string InsertHabitTable = "INSERT INTO habit (name, desc, chat_id, is_good) " +
                                                  "VALUES (@name, @desc, @chatid, @isgood); " +
                                                  "SELECT last_insert_rowid();";

        public const string InsertDaysTable = "INSERT INTO days (id, on_monday, on_tuesday, on_wednesday, on_thursday, on_friday, on_saturday, on_sunday) " +
                                              "VALUES (@id, @monday, @tuesday, @wednesday, @thursday, @friday ,@saturday ,@sunday);";

        public const string InsertTimeTable = "INSERT INTO time (id, time) " +
                                              "VALUES (@id, @time); ";

    }
}

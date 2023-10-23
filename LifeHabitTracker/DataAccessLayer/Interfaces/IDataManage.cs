using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LifeHabitTracker.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс взаимодействия сервера с БД
    /// </summary>
    public interface IDataManage
    {
        /// <summary>
        /// Записать данные о привычке в таблицу
        /// </summary>
        /// <returns>Оценка результата выполнения записи</returns>
        public bool WriteHabitInfo(string name, string desc, long chatId, int isGood, Dictionary<string, int> daysAndReminds, IReadOnlyCollection<string> times);
    }
}

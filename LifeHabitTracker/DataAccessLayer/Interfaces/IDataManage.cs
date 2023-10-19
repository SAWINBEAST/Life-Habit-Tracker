using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool WriteHabitInfo();
    }
}

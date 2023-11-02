using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Entities.PreparedData
{
    /// <summary>
    /// Класс подготовленной информации для таблицы times
    /// </summary>
    public class PreparedTimesTableData
    {
        /// <summary>
        /// Коллекция времени напоминания о привычке
        /// </summary>
        public IReadOnlyCollection<string> Times { get; set; }

    }
}

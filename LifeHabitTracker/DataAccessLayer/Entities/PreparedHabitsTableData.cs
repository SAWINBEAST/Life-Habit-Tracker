using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Entities
{
    /// <summary>
    /// Класс подготовленной информации для таблицы habits
    /// </summary>
    public class PreparedHabitsTableData
    {
        /// <summary>
        /// Идентификатор привычки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название привычки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание привычки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор чата клиента, пользующего привычкой
        /// </summary>
        public int ChatId { get; set; }

        /// <summary>
        /// Тип привычки
        /// </summary>
        public bool IsGood { get; set; }

    }
}

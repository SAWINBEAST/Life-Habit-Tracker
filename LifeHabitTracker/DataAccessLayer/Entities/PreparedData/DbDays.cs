namespace LifeHabitTracker.DataAccessLayer.Entities.PreparedData
{
    /// <summary>
    /// Класс подготовленной информации для таблицы days
    /// </summary>
    public class DbDays
    {
        /// <summary>
        /// Понедельник
        /// </summary>
        public bool OnMonday { get; set; }

        /// <summary>
        /// Вторник
        /// </summary>
        public bool OnTuesday { get; set; }

        /// <summary>
        /// Среда
        /// </summary>
        public bool OnWednesday { get; set; }

        /// <summary>
        /// Четверг
        /// </summary>
        public bool OnThursday { get; set; }

        /// <summary>
        /// Пятница
        /// </summary>
        public bool OnFriday { get; set; }

        /// <summary>
        /// Суббота
        /// </summary>
        public bool OnSaturday { get; set; }

        /// <summary>
        /// Воскресенье
        /// </summary>
        public bool OnSunday { get; set; }
    }
}
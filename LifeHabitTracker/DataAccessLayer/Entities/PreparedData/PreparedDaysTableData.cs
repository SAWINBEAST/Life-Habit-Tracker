namespace LifeHabitTracker.DataAccessLayer.Entities.PreparedData
{
    /// <summary>
    /// Класс подготовленной информации для таблицы days
    /// </summary>
    public class PreparedDaysTableData
    {
        /// <summary>
        /// Схема День <-> Истинность напоминания для записи в таблицу 
        /// </summary>
        public IDictionary<string, int> DaysAndReminds { get; set; } = new Dictionary<string, int>()
            {
                {"monday", 0 },
                {"tuesday", 0 },
                {"wednesday", 0 },
                {"thursday", 0 },
                {"friday", 0 },
                {"saturday", 0 },
                {"sunday", 0 }
            };
    }
}

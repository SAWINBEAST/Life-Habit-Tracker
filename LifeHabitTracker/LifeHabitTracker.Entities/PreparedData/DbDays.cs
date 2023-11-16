namespace LifeHabitTracker.Entities.PreparedData
{
    /// <summary>
    /// Класс подготовленной информации для таблицы days
    /// </summary>
    public class DbDays
    {
        /// <summary>
        /// Схема День <-> Истинность напоминания для записи в таблицу 
        /// </summary>
        public IDictionary<string, int> DaysAndReminds { get; set; } = new Dictionary<string, int>()
            {
                {EnglishDays.Monday, 0 },
                {EnglishDays.Tuesday, 0 },
                {EnglishDays.Wednesday, 0 },
                {EnglishDays.Thursday, 0 },
                {EnglishDays.Friday, 0 },
                {EnglishDays.Saturday, 0 },
                {EnglishDays.Sunday, 0 }
            };
    }
}


namespace LifeHabitTracker.DataAccessLayer.Entities.PreparedData
{
    /// <summary>
    /// Класс подготовленной информации для таблицы times
    /// </summary>
    public class DbTimes
    {
        /// <summary>
        /// Коллекция времени напоминания о привычке
        /// </summary>
        public IReadOnlyCollection<string> Times { get; set; }
    }
}
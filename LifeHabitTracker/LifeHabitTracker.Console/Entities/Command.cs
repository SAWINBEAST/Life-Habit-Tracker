namespace LifeHabitTrackerConsole.Entities
{
    /// <summary>
    /// Команды бота
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Запуск бота
        /// </summary>
        public const string Start = "/start";

        /// <summary>
        /// Создание
        /// </summary>
        public const string CreateHabit = "/createHabit";

        /// <summary>
        /// Получить привычки
        /// </summary>
        public const string Habits = "/habits";
    }
}
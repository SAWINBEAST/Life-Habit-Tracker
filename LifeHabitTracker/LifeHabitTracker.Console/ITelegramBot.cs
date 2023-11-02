namespace LifeHabitTrackerConsole
{
    /// <summary>
    /// Интерфейс функционала бота
    /// </summary>
    internal interface ITelegramBot
    {
        /// <summary>
        /// Запуск Бота
        /// </summary>
        Task LaunchAsync();
    }
}

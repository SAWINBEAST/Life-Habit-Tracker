namespace LifeHabitTrackerConsole
{
    /// <summary>
    /// Интерфейс функционала бота
    /// </summary>
    internal interface IBot
    {
        /// <summary>
        /// Запуск Бота
        /// </summary>
        /// <returns></returns>
        Task LaunchAsync();
    }
}

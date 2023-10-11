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
        /// <returns>Успешно выполненные отлов запроса и его последующая обработка</returns>
        Task LaunchAsync();
    }
}

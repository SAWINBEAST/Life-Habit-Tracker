namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.State
{
    /// <summary>
    /// Контекст процесса создания привычки
    /// </summary>
    public interface IContextHabitCreation
    {
        /// <summary>
        /// Состояние контекста
        /// </summary>
        public IHabitCreationState State { get; set; }

        /// <summary>
        /// Запустить работу контекста
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public Task StartContextAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Обработать ответ пользователя в рамках создания привычки
        /// </summary>
        /// <param name="userResponse">Ответ пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public Task HandleUserResponseAsync(string userResponse, CancellationToken cancellationToken);
    }
}
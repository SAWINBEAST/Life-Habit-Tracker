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

    }
}
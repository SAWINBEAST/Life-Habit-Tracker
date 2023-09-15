using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <inheritdoc cref="IHabitCreationState"/>
    public abstract class HabitCreationState : IHabitCreationState
    {
        /// <summary>
        /// Сообщение на запрос необходимых данных
        /// </summary>
        protected string DataRequestMessage;

        /// <inheritdoc/>
        public string GetDataRequest() => DataRequestMessage;

        /// <inheritdoc/>
        public abstract (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit);

        /// <summary>
        /// Перейти к новому состоянию
        /// </summary>
        /// <returns>Новое состояние</returns>
        protected abstract IHabitCreationState TransitionToNewState();
    }
}
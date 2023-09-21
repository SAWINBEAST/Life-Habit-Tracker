using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <summary>
    /// Контекст определяет интерфейс, представляющий интерес для клиентов. Онтакже хранит ссылку на экземпляр подкласса Состояния, который отображаеттекущее состояние Контекста
    /// </summary>
    public class ContextHabitCreation : IContextHabitCreation
    {

        /// <summary>
        /// Создаваемая привычка
        /// </summary>
        public Habit Habit { get; }

        /// <inheritdoc/>
        public IHabitCreationState State { get; set; }

        /// <summary>
        /// Данные чата, в рамках которого существует контекст
        /// </summary>
        public ChatInfo ChatInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        private event Func<ChatInfo, string, bool, Habit, CancellationToken, Task> DataCompleted;

        public ContextHabitCreation(ChatInfo chatInfo, Func<ChatInfo, string, bool, Habit, CancellationToken, Task> handleRequestFunc)
        {
            Habit = new Habit();
            State = new NameState();
            ChatInfo = chatInfo;
            DataCompleted += handleRequestFunc;
        }

        /// <inheritdoc/>
        public async Task StartContextAsync(CancellationToken cancellationToken)
            => await DataCompleted(ChatInfo, State.GetDataRequest(), false, null, cancellationToken);

        /// <inheritdoc/>
        public async Task HandleUserResponseAsync(string userResponse, CancellationToken cancellationToken)
        {
            var (infoMessage, isFinish) = State.HandleData(this, userResponse, Habit);
            await DataCompleted(ChatInfo, infoMessage, isFinish, Habit, cancellationToken);
        }
    }
}
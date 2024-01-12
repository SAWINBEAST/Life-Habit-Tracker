using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <summary>
    /// Состояние получения описания привычки
    /// </summary>
    internal class DescState : HabitCreationState
    {
        public DescState() => DataRequestMessage = "Введите Описание привычки.";

        /// <inheritdoc/>
        public override (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {
            Console.WriteLine($"Введённые данные для Описания привычки: {data}");

            if (data.Length > 100)
                return ("Наименование Описания должно быть не более 100 символов. Попробуйте ещё раз.", false);

            habit.Description = data;

            context.State = TransitionToNewState();

            return ($"Описание привычки: {data}.\n{context.State.GetDataRequest()}", false);
        }

        /// <inheritdoc/>
        protected override IHabitCreationState TransitionToNewState() => new TypeState();
    }
}


using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <summary>
    /// Состояние получения наименования привычки
    /// </summary>
    internal class NameState : HabitCreationState
    {
        public NameState() => DataRequestMessage = "Ввведите наименование привычки.";

        /// <inheritdoc/>
        public override (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {
            Console.WriteLine($"Введённые данные для наименования привычки: {data}");

            if (data.Length > 27)
                return ("Наименование привычки должно быть не более 27 символов. Попробуйте ещё раз.", false);

            habit.Name = data;

            context.State = TransitionToNewState();

            return ($"Наименование привычки: {data}.\n{context.State.GetDataRequest()}", false);
        }

        /// <inheritdoc/>
        protected override IHabitCreationState TransitionToNewState() => new DescState();
    }
}
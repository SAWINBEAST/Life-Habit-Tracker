using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;


namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeState : HabitCreationState
    {
        public TypeState() => DataRequestMessage = "Ввведите Тип привычки.";

        /// <inheritdoc/>
        public override (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {
            const string goodHabit = "хорошая";
            const string badHabit = "плохая";

            Console.WriteLine($"Введённые данные для Типа привычки: {data}");

            if (data != goodHabit && data != badHabit)
                return("Существует два типа привычки: Хорошая и Плохая. \nПопробуйте ещё раз)", false);

            habit.Type = data;

            if (data == goodHabit)
            {
                context.State = TransitionToNewState();
                return ($"Тип привычки: {data}.\n{context.State.GetDataRequest()}", false);
            }

            return($"Тип привычки: {data}.", true);
        }

        /// <inheritdoc/>
        protected override IHabitCreationState TransitionToNewState() => new DateState();
    }
}
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;


namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <summary>
    /// Состояние получения типа привычки
    /// </summary>
    public class TypeState : HabitCreationState
    {
        public TypeState() => DataRequestMessage = "Ввведите Тип привычки.\n(Она у вас Хорошая или Плохая?)";

        /// <inheritdoc/>
        public override (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {

            const string goodHabit = "хорошая";
            const string badHabit = "плохая";

            string dataLower = data.ToLower();

            Console.WriteLine($"Введённые данные для Типа привычки: {data}");

            if (dataLower != goodHabit && dataLower != badHabit)
                return("Существует два типа привычки: Хорошая и Плохая. \nПопробуйте ещё раз)", false);

            habit.Type = dataLower;

            if (dataLower == goodHabit)
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
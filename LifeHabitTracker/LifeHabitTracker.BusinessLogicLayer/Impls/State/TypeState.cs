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
            Console.WriteLine($"Введённые данные для Типа привычки: {data}");

            switch(data.ToLower())
            {
                case "хорошая":
                    habit.Type = data;

                    context.State = TransitionToNewState();

                    return ($"Тип привычки: {data}.\n{context.State.GetDataRequest()}", false);

                case "плохая":
                    habit.Type = data;

                    return ($"Тип привычки: {data}.", true);

                default:
                    return ("Существует два типа привычки: Хорошая и Плохая. \nПопробуйте ещё раз)", false);

            }
           
        }

        /// <inheritdoc/>
        protected override IHabitCreationState TransitionToNewState() => new DateState();
    }
}
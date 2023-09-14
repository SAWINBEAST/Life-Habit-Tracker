using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{

    public class NameState : IState
    {
        private ContextHabitCreation _context;

        public const string nextState = "Тип привычки";

        public void SetContext(ContextHabitCreation context)
        {
            this._context = context;
        }

        public void HandleNextState()
        {
            this._context.TransitionTo(new TypeState());
        }

        public string HandleWriteValue(string data)
        {
            Console.WriteLine("Выполняем запись имени");
            _context.habit.Name = data;
            return $"Введите {nextState}.";
        }


    }
}

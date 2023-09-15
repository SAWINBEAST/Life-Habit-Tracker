using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeState : HabitCreationState
    {
        public TypeState() => DataRequestMessage = "Ввведите тип привычки.";

        public override (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {
            throw new NotImplementedException();
        }

        protected override IHabitCreationState TransitionToNewState()
        {
            throw new NotImplementedException();
        }
    }
}
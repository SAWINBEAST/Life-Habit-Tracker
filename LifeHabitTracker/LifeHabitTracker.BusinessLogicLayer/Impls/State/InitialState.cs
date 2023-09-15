using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    public class InitialState : IHabitCreationState
    {
        public string GetDataRequest()
        {
            throw new NotImplementedException();
        }

        public (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {
            throw new NotImplementedException();
        }
    }
}

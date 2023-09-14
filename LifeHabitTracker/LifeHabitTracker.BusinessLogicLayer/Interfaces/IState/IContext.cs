using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.IState
{
    public interface IContextHabitCreation
    {
        public Habit GetUsedHabit();    
/*        public string IsEmpty();
*/
        public void TransitionTo(IState state);

        public void RequestNextState();

        public string RequestWriteValue(string message);


    }
}

using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    public class DescState : IState
    {
        private ContextHabitCreation _context;

        public const string nextState = "Дату привычки";


        public void SetContext(ContextHabitCreation context)
        {
            this._context = context;
        }

        public void HandleNextState()
        {
            this._context.TransitionTo(new DateState());
        }

        public string HandleWriteValue(string data)
        {
            Console.WriteLine("Выполняем запись описания");
            _context.habit.Desc = data;
            return $"Введите {nextState}.";
        }

    }
}

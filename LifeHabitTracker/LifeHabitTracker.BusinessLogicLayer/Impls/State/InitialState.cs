using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    public class InitialState : IState
    {
        private ContextHabitCreation _context;

        public const string nextState = "Название привычки";

        public InitialState() { }

        public void SetContext(ContextHabitCreation context)
        {
            this._context = context;
        }

        public void HandleNextState()
        {
            this._context.TransitionTo(new NameState());
        }

        public string HandleWriteValue(string data)
        {
            Console.WriteLine("Начали создавать привычку");
            var stub = data;
            return $"Введите {nextState}.";
        }


    }
}

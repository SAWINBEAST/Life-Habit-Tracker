using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    public class DateState : IState
    {
        private ContextHabitCreation _context;

        public const string nextState = "Все данные введены.";

        public void SetContext(ContextHabitCreation context)
        {
            this._context = context;
        }

        public void HandleNextState()
        {
            //Вот тут мне надо удалить мой контекст, так как привычка создана.
            this._context.TransitionTo(new InitialState());
        }

        public string HandleWriteValue(string data)
        {
            Console.WriteLine("Выполняем запись даты");
            _context.habit.Date = data;
            return $"{nextState}";
        }

    }
}

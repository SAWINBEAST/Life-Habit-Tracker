using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    public class TypeState : IState
    {

        private ContextHabitCreation _context;

        public const string nextState = "Описание привычки";


        public void SetContext(ContextHabitCreation context)
        {
            this._context = context;
        }

        public void HandleNextState()
        {
            this._context.TransitionTo(new DescState());
        }

        public string HandleWriteValue(string data)
        {
            Console.WriteLine("Выполняем запись типа");
            _context.habit.Type = data;
            return $"Введите {nextState}.";
        }

    }
}

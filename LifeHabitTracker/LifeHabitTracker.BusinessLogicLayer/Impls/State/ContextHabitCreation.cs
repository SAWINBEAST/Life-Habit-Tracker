using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    /// <summary>
    /// Контекст определяет интерфейс, представляющий интерес для клиентов. Онтакже хранит ссылку на экземпляр подкласса Состояния, который отображаеттекущее состояние Контекста
    /// </summary>
    public class ContextHabitCreation: IContextHabitCreation
    {
        /// <summary>
        /// Ссылка на текущее состояние Контекста.
        /// </summary>
        private IState _state = null;

        public Habit habit = new Habit();


        public ContextHabitCreation(IState state)
        {
            this.TransitionTo(state);
        } 

        /// <summary>
        ///  Изменение Состояния объекта во время выполнения.
        /// </summary>
        /// <param name="state"></param>
        public void TransitionTo(IState state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        /// <summary>
        /// Делегирование части своего поведения текущему объекту состояния.
        /// </summary>
        public void RequestNextState()
        {
            this._state.HandleNextState();
        }

        public string RequestWriteValue(string message)
        {
            return this._state.HandleWriteValue(message);
        }

        public Habit GetUsedHabit()
        {
            return habit;
        }

       /* public string IsEmpty()
        {
            if (habit.Name == null)
                return "Сейчас создадим. \n\nВведите название Привычки";
            else
                return null;

        }*/
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IOriginator"/>.
    public class Originator : IOriginator
    {
        public string State {  get; set; }


        /// <inheritdoc/>
        public Memento CreateMemento(string state)
        {
            return new Memento(state);
        }

        /// <summary>
        /// Записывает выведенное из Хранилица состояние в промежуточное свойство
        /// </summary>
        /// <param name="memento"></param>
        public void SetMemento(Memento memento)
        {
            State = memento.State;
        }


        public string GetMemento() 
        {
            return State;
        }



    }
}

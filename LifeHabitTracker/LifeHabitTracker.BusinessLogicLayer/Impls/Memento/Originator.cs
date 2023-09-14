using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="IOriginator"/>.
    public class Originator : IOriginator
    {
        public string State { get; set; }

        /// <inheritdoc/>
        public Memento CreateMemento(string state)
        {
            return new Memento(state);
        }

        ///<inheritdoc/>
        public void SetMemento(string memento)
        {
            State = memento;
        }

        ///<inheritdoc/>
        public string GetMemento()
        {
            return State;
        }



    }
}

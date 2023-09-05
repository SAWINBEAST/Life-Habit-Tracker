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

       
        public Originator(Memento memento)
        {
            State = memento.State;
        }

        /// <inheritdoc/>
        public Memento CreateMemento()
        {
            return new Memento(State);
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IMemento"/>
    public class Memento
    {
        public string State { get; private set; }
        public Memento(string state) 
        {
            this.State = state;
        }
    }
}

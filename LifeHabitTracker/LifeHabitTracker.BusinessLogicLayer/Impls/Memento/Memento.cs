using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="IMemento"/>
    public class Memento : IMemento
    {
        public string State { get; private set; }
        public Memento(string state)
        {
            State = state;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IMemento"/>
    public class Memento : IMemento
    {
        public string State { get; private set; }
/*        public string Username { get; private set; }
*/        public Memento(string state/*, string username*/) 
        {
            State = state;
/*            Username = username;
*/        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="ICaretaker"/>
    public class Caretaker:ICaretaker
    {
        internal Memento Memento { get; set; }
    }
}

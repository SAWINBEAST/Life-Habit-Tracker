using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IDataHandler"/>
    public class DateHandler : IDataHandler
    {
        public IDataHandler Successor { get; set; }

        /// <inheritdoc/>
        public void AppointSuccesor(IDataHandler handler)
        {
            if (handler == null)
            {
                Successor = handler;
            }
        }

        /// <inheritdoc/>
        public void Handle(Reciever receiver)
        {
            if (receiver.DateExistence == false)
            {
                Console.WriteLine("Выполняем запись даты");
                receiver.DateExistence = true;
            }

            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{    
    /// <inheritdoc cref="IDataHandler"/>
    public class NameHandler : IDataHandler
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
            if (receiver.NameExistence == false)
            {
                Console.WriteLine("Выполняем запись имени");
                receiver.NameExistence = true;
            }
                
            else if (Successor != null)
                Successor.Handle(receiver);
        }

    }
}

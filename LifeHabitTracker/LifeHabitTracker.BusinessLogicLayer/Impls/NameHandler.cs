using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="IDataHandler"/>
    public class NameHandler : IDataHandler
    {
        public IDataHandler Successor { get; set; }

        /// <inheritdoc/>
        public void AppointSuccesor(IDataHandler handler)
        {
            if (handler != null)
            {
                Successor = handler;
            }

        }

        /// <inheritdoc/>
        public void Handle(IReciever receiver, IHabitService habitService, string data)
        {
            if (receiver.GetNameExistence() == false)
            {
                Console.WriteLine("Выполняем запись имени");
                habitService.SetName(data);
                receiver.ChangeExistence(1);
            }

            else if (Successor != null)
                Successor.Handle(receiver, habitService, data);
      
                
        }

    }
}

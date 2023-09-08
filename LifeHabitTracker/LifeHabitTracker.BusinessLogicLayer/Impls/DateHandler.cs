using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="IDataHandler"/>
    public class DateHandler : IDataHandler
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
            if (receiver.GetDateExistence() == false)
            {
                Console.WriteLine("Выполняем запись даты");
                habitService.SetDate(data);
                receiver.ChangeExistence(4);
            }

            else if (Successor != null)
                Successor.Handle(receiver, habitService, data);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    public class DateHandler : IDataHandler
    {
        public DateHandler Successor { get; set; }
        public void Handle(Receiver receiver)
        {
            if (receiver.DateExistence == false)
            {
                Console.WriteLine("Выполняем запись имени");
                receiver.DateExistence = true;
            }

            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
}

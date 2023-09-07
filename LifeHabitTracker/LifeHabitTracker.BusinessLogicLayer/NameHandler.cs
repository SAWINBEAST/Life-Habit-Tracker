using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    public class NameHandler : IDataHandler
    {
        public NameHandler Successor { get; set; }
        public void Handle(Receiver receiver)
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

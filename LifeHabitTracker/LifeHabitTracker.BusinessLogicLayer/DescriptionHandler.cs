using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    public class DescriptionHandler : IDataHandler
    {
        public DescriptionHandler Successor { get; set; }
        public void Handle(Receiver receiver)
        {
            if (receiver.DescExistence == false)
            {
                Console.WriteLine("Выполняем запись имени");
                receiver.DescExistence = true;
            }

            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
}

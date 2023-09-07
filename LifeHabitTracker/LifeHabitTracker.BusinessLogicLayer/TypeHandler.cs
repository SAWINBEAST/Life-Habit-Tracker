using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    public class TypeHandler : IDataHandler
    {
        public TypeHandler Successor { get; set; }
        public void Handle(Receiver receiver)
        {
            if (receiver.TypeExistence == false)
            {
                Console.WriteLine("Выполняем запись имени");
                receiver.TypeExistence = true;
            }

            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
}

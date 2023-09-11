using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="IDataHandler"/>
    public class TypeHandler : ITypeHandler, IDataHandler
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
        public async Task Handle(IReciever receiver, IHabitService habitService, string data)
        {
            if (receiver.GetTypeExistence() == false)

            {
                Console.WriteLine("Выполняем запись типа");
                habitService.SetType(data);
                receiver.ChangeExistence(2);
            }

            else if (Successor != null)
                 await Successor.Handle(receiver, habitService, data);
        }
    }
}

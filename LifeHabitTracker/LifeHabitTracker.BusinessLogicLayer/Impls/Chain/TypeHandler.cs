using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IChain;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Chain
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
/*                Console.WriteLine("Выполняем запись типа");
                habitService.SetType(data);
                receiver.ChangeExistence(2);*/
            }

            else if (Successor != null)
                await Successor.Handle(receiver, habitService, data);
        }
    }
}

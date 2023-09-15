using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IChain;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Chain
{
    /// <inheritdoc cref="INameHandler"/>
    public class NameHandler : INameHandler, IDataHandler
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
            if (receiver.GetNameExistence() == false)
            {
                Console.WriteLine("Выполняем запись имени");
                habitService.SetName(data);
                receiver.ChangeExistence(1);
            }

            else if (Successor != null)
                await Successor.Handle(receiver, habitService, data);


        }

    }
}

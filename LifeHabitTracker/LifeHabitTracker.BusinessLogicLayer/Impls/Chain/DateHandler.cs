using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IChain;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Chain
{
    /// <inheritdoc cref="IDateHandler"/>
    public class DateHandler : IDateHandler, IDataHandler
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
            if (receiver.GetDateExistence() == false)
            {
                Console.WriteLine("Выполняем запись даты");
                habitService.SetDate(data);
                receiver.ChangeExistence(4);
            }

            else if (Successor != null)
                await Successor.Handle(receiver, habitService, data);

        }
    }
}

using LifeHabitTracker.BusinessLogicLayer.Interfaces.IChain;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Chain
{
    /// <inheritdoc cref="IDescHandler"/>
    public class DescriptionHandler : IDescHandler, IDataHandler
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

        public Task Handle(IReciever receiver, IHabitService habitService, string data)
        {
            throw new NotImplementedException();
        }
    }
}

using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.IChain
{
    /// <summary>
    /// Функционал обработчика событий
    /// </summary>
    public interface ITypeHandler
    {
        /// <summary>
        /// Функционал обработчика событий
        /// </summary>
        /// <summary>
        /// Обрабатывает запроса от бота 
        /// </summary>
        /// <param name="receiver"></param>
        public Task Handle(IReciever receiver, IHabitService habitService, string data);

        /// <summary>
        /// Добавляет приемника текущему обработчику
        /// </summary>
        /// <param name="handler"></param>
        public void AppointSuccesor(IDataHandler handler);
    }
}

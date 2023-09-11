using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Impls;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Общий Функционал обработчика событий
    /// </summary>
    public interface IDataHandler
    {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Функционал обработчика событий
    /// </summary>
    public interface INameHandler
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

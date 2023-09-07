using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Функционал обработчика событий
    /// </summary>
    public interface IDataHandler
    {
        /// <summary>
        /// Обрабатывает запроса от бота 
        /// </summary>
        /// <param name="receiver"></param>
        public void Handle(Reciever receiver);

        /// <summary>
        /// Добавляет приемника текущему обработчику
        /// </summary>
        /// <param name="handler"></param>
        public void AppointSuccesor(IDataHandler handler);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Интерфейс получателя контекста от бота
    /// </summary>
    internal interface IContext
    {
        /// <summary>
        /// Метод получение Контекста от Бота
        /// </summary>
        /// <returns></returns>
        Task GetContext();
    }
}

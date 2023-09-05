using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    ///  Интерфейс состояния работы с ботом + Создатель хранителя
    /// </summary>
    public interface IOriginator
    {
        /// <summary>
        /// Сохранение состояния
        /// </summary>
        /// <returns></returns>
        public Memento CreateMemento();

    }
}

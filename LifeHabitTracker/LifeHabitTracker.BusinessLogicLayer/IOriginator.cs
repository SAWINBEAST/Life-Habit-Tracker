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
        public Memento CreateMemento(string state);

        /// <summary>
        /// Возвращение дежурного состояния
        /// </summary>
        /// <returns></returns>
        public string GetMemento();

        /// <summary>
        /// Записывает выведенное из Хранилица состояние в промежуточное свойство
        /// </summary>
        /// <param name="memento"></param>
        public void SetMemento(string memento);


    }
}

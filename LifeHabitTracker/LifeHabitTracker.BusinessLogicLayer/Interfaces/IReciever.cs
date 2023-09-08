using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Функционал хранителя состояний данных привычки
    /// </summary>
    public interface IReciever
    {
        /// <summary>
        /// Меняет состояние добавленных данных
        /// </summary>
        /// <param name="i"></param>
        public void ChangeExistence(int i);

        /// <summary>
        /// Выдает значение, которое позволяет понять, существует ли имя
        /// </summary>
        /// <returns></returns>
        public bool GetNameExistence();

        /// <summary>
        /// Выдает значение, которое позволяет понять, существует ли тип
        /// </summary>
        /// <returns></returns>
        public bool GetTypeExistence();

        /// <summary>
        /// Выдает значение, которое позволяет понять, существует ли описание
        /// </summary>
        /// <returns></returns>
        public bool GetDescExistence();

        /// <summary>
        /// Выдает значение, которое позволяет понять, существует ли дата
        /// </summary>
        /// <returns></returns>
        public bool GetDateExistence();



    }
}

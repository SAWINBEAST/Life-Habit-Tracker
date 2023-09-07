using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
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

        public bool GetNameExistence();

        public bool GetTypeExistence();

        public bool GetDescExistence();

        public bool GetDateExistence();



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Impls;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Хранитель объекта Memento
    /// </summary>
    public interface ICaretaker
    {
        /// <summary>
        /// Добавление нового состояния определенного юзера в словарь
        /// </summary>
        /// <param name="username"></param>
        /// <param name="state"></param>
        public void AddUserState(string username, Memento state);

        /// <summary>
        /// Вывод текущего состояния определенного пользователя
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetUserState(string username);

        /// <summary>
        /// Удаление неактуального состояния определенного пользователя
        /// </summary>
        /// <param name="username"></param>
        public void RemoveUserState(string username);

    }
}

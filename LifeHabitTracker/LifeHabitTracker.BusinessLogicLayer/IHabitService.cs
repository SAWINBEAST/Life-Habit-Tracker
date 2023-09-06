using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Описывает функционал Привычки
    /// </summary>
    public interface IHabitService
    {
        /// <summary>
        /// Загрузка имени
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name);

        /// <summary>
        /// Создание привычки
        /// </summary>
        /// <returns></returns>
        void CreateHabit(string name, string desc, string type, DateTime date);

        /// <summary>
        /// Рассказывает о привычке. Что она из себя представляет, какие есть составляющие
        /// </summary>
        List<object> GetInfo();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Описывает функционал Привычки
    /// </summary>
    internal interface IHabitService
    {
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

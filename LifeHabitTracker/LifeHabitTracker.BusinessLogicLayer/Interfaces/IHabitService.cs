using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces
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
        /// Загрузка типа
        /// </summary>
        /// <param name="name"></param>
        public void SetType(string type);

        /// <summary>
        /// Загрузка описания
        /// </summary>
        /// <param name="name"></param>
        public void SetDesc(string desc);

        /// <summary>
        /// Загрузка даты
        /// </summary>
        /// <param name="name"></param>
        public void SetDate(string date);


        /// <summary>
        /// Создание привычки
        /// </summary>
        /// <returns></returns>
        void CreateHabit(string name, string desc, string type, string date);

        /// <summary>
        /// Рассказывает о привычке. Что она из себя представляет, какие есть составляющие
        /// </summary>
        string GetInfo();
    }
}

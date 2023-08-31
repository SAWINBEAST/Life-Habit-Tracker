using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Описывает функционал Привычки
    /// </summary>
    internal interface IHabit
    {
        /// <summary>
        /// Рассказывает о привычке. Что она из себя представляет, какие есть составляющие
        /// </summary>
        List<object> GetInfo();

        /// <summary>
        /// Выдает напоминание в Бот, что нужно выполнить ее или какую-то ее часть(Составляющую). Выдает в определенную дату
        /// </summary>
        Task RemindAbout();

        /// <summary>
        /// Записывает успешное выполнение привычки. Отдает в Бот ответ. При отсутствии сигнала о выполнении - записывает невыполнение привычки
        /// </summary>
        Task CommitExecution();

        /// <summary>
        /// Считает количество успешных выполнений привычки за месяц. Выдает заключение о привитии или непривитии деятельности человеку
        /// </summary>
        Task GetConclusion();
    }
}

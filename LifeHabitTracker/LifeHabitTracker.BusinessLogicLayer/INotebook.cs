using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Описывает функционал записных сущностей, которые хранят некоторые данные и изредка напоминают о них
    /// </summary>
    internal interface INotebook
    {
        /// <summary>
        /// Добавить новую запись 
        /// </summary>
        /// <returns></returns>
        Task Update();

        /// <summary>
        /// Удалить ненужную запись
        /// </summary>
        /// <returns></returns>
        Task Delete();

        /// <summary>
        /// Осуществить задуманное записью
        /// </summary>
        /// <returns></returns>
        Task Perform();

        /// <summary>
        /// Выдать напоминание Боту в определенную дату или день недели
        /// </summary>
        /// <returns></returns>
        Task RemindAbout();

    }
}

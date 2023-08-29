using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace LifeHabitTrackerConsole
{
    internal interface IBot
    {
        /// <summary>
        /// Запуск Бота
        /// </summary>
        /// <returns></returns>
        Task Launch();
    }
}

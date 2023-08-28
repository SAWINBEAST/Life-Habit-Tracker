using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace LifeHabitTrackerConsole
{
    internal interface IBot
    {
        static readonly ITelegramBotClient bot;

        /// <summary>
        /// Логика выдачи ответа Бота на введённую команду + Логирование введённой команды
        /// </summary>
        /// <returns></returns>
        Task HandleUpdateAsync() { return (Task)bot;}

        /// <summary>
        /// Логирование ошибки при взаимодействии пользователя с ботом
        /// </summary>
        /// <returns></returns>
        Task HandleErrorAsync() { return (Task)bot; }

        /// <summary>
        /// Запуск Бота
        /// </summary>
        /// <returns></returns>
        Task Launch() { return (Task)bot; }
    }
}

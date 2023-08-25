using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace LifeHabitTrackerConsole
{
    internal interface IBot
    {
        static ITelegramBotClient bot;

        //Логика выдачи ответа Бота на введённую команду + Логирование введённой команды
        static Task HandleUpdateAsync() { return (Task)bot; }

        //Логирование ошибки при взаимодействии пользователя с ботом
        static Task HandleErrorAsync() { return (Task)bot; }

        //Запуск Бота
        Task Launch() { return (Task)bot; }
    }
}

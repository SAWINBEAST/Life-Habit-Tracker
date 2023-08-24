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
        static Task HandleUpdateAsync() { return (Task)bot; }
        static Task HandleErrorAsync() { return (Task)bot; }

        static Task Launch() { return (Task)bot; }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace LifeHabitTrackerConsole
{ 

    class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Приложение запущено.");

            await TelegramBot.Launch();

            Console.ReadLine();
        }
    }
}


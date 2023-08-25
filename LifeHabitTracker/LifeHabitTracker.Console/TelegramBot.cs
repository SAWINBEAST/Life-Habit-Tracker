using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Newtonsoft.Json;

namespace LifeHabitTrackerConsole
{
    internal class TelegramBot : IBot
    {
        const string token = "6694819520:AAGeqWeAEHH3m7kJ4jphSq6IjOrWU_zSh64";
        static ITelegramBotClient bot = new TelegramBotClient(token);

        //Логика выдачи ответа Бота на введённую команду + Логирование введённой команды
        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                Console.WriteLine($"Cooбщение от пользователя: {message?.Text}");
                if (message?.Text?.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        //Логирование ошибки при взаимодействии пользователя с ботом
        private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        //Запуск Бота
        public async Task Launch()
        {
            Task<User> botName = bot.GetMeAsync();
            await botName;
            Console.WriteLine("Запущен бот " + botName.Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }
        
    }
}

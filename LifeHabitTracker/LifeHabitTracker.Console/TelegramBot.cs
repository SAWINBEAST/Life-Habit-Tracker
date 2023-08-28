using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LifeHabitTrackerConsole
{
    /// <inheritdoc cref="IBot"/>.
    internal class TelegramBot : IBot
    {
        private const string Token = "6694819520:AAGeqWeAEHH3m7kJ4jphSq6IjOrWU_zSh64";
        private readonly ITelegramBotClient bot = new TelegramBotClient(Token);

        /// <inheritdoc/>.
        /// <summary>
        /// Логика выдачи ответа Бота на введённую команду + Логирование введённой команды
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                var username = message.From.Username;
                Console.WriteLine($"Cooбщение от пользователя {username}: {message?.Text}");
                if (message?.Text?.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"Добро пожаловать на борт,{username}");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, $"Привет-привет, {username}");
            }
        }

        /// <inheritdoc/>.
        /// <summary>
        /// Логирование ошибки при взаимодействии пользователя с ботом
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        /// <inheritdoc/>.
        /// <summary>
        /// Запуск Бота
        /// </summary>
        /// <returns></returns>
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

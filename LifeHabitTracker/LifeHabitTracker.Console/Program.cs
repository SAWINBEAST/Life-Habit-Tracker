using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
//using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace TelegramBotExperiments
{

    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6694819520:AAGeqWeAEHH3m7kJ4jphSq6IjOrWU_zSh64");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            /*string json =Newtonsoft.Json.JsonConvert.SerializeObject(update);
            JsonObject restoredJson = JsonSerializer.Deserialize<JsonObject>(json);*/
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            { 
                var message = update.Message;
                Console.WriteLine($"Cooбщение от пользователя: {message.Text}");
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
/*
{ "update_id":376654527,
  "message":{ 
        "message_id":10,
        "from":{ 
            "id":722520401,
            "is_bot":false,
            "first_name":"Сан",
            "last_name":"Сеич",
            "username":"SavinCrew",
            "language_code":"ru"},
        "date":1692790284,
        "chat":{ 
            "id":722520401,
            "type":"private",
            "username":"SavinCrew",
            "first_name":"Сан",
            "last_name":"Сеич"},
        "text":"алоха"} 
}*/


using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Newtonsoft.Json;
using LifeHabitTracker.BusinessLogicLayer;
using System.Threading.Channels;

namespace LifeHabitTrackerConsole
{
    /// <inheritdoc cref="IBot"/>.
    internal class TelegramBot : IBot
    {
        IHabitService habitService;
        IOriginator originator;
        ICaretaker caretaker;
        //IMemento memento;

        /// <summary>
        /// API-токен бота телеграм
        /// </summary>
        private const string Token = "6694819520:AAGeqWeAEHH3m7kJ4jphSq6IjOrWU_zSh64";

        /// <summary>
        /// Объект клиента бота Телеграм
        /// </summary>
        private readonly ITelegramBotClient _bot = new TelegramBotClient(Token);

        public TelegramBot(IHabitService habitService, ICaretaker caretaker, IOriginator originator /*,IMemento memento*/)
        {
            this.habitService = habitService;
            this.originator = originator;
            this.caretaker = caretaker;
            //this.memento = memento;
        }

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

                var currentState = originator.CreateMemento(message.Text);
                if (caretaker.GetUserState(username).State == null)
                {
                    caretaker.AddUserState(username, currentState);
                }

                var receivedState = caretaker.GetUserState(username);
                
                if(receivedState.State.StartsWith("/"))
                {
                    //caretaker.RemoveUserState(username);


                    if (message?.Text?.ToLower() == "/start")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, $"Добро пожаловать в Привычковную,{username}");
                        caretaker.RemoveUserState(username);
                        caretaker.AddUserState(username, currentState);


                        //вот тут прописать логику выпадания меню. См. заметки в ТГ
                        return;
                    }
                    else if (message?.Text?.ToLower() == "/создать")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, $"Сейчас создадим");

                        await botClient.SendTextMessageAsync(message.Chat, $"Введите название Привычки");
                        caretaker.RemoveUserState(username);
                        caretaker.AddUserState(username, currentState);
                    }
                    //Вот тут прописать логику других команд, используя switch case


                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat, $"Привет-привет, {username} :)\nВведи команду, используя \"/\"");
                    }
                }
                else
                {
                    if (true /*проверка наличия введённого имени через Хранитель имени*/)
                    {
                        //Получение данных и запись будут тут
                        habitService.SetName(message.Text);

                        await botClient.SendTextMessageAsync(message.Chat, $"Теперь введите её тип (хорошая/плохая)");
                        caretaker.RemoveUserState(username);
                        caretaker.AddUserState(username, currentState);


                    }
                    else if (true /*проверка наличия введённого типа через Хранитель типа*/ )
                    {
                        //Получение данных и запись будут тут
                        await botClient.SendTextMessageAsync(message.Chat, $"А теперь введите её описание");
                    }
                    else if (true /*проверка наличия введённого описания через Хранитель описания*/ )
                    {
                        //Получение данных и запись будут тут

                        await botClient.SendTextMessageAsync(message.Chat, $"По каким дням или датам вы хотите получать напоминания ?");
                    }
                    else if (true /*проверка наличия введённой даты через Хранитель даты*/ )
                    {
                        //Получение данных и запись будут тут

                        await botClient.SendTextMessageAsync(message.Chat, $"Прекрасно. Привычка создана!");

                        //удаление Хранителей будет тут
                    }

                }

            }

        }


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

        /// <inheritdoc/>
        public async Task LaunchAsync()
        {
            var botName = await _bot.GetMeAsync();
            Console.WriteLine("Запущен бот " + botName.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };


            //начать получать
            _bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }
        
    }
}

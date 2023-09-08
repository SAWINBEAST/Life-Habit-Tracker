using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Newtonsoft.Json;
using System.Threading.Channels;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTrackerConsole
{
    /// <inheritdoc cref="IBot"/>.
    internal class TelegramBot : IBot
    {
        IHabitService habitService;

        IOriginator originator;
        ICaretaker caretaker;

        IReciever reciever;
        IDataHandler nameHandler;
        IDataHandler typeHandler;
        IDataHandler descHandler;
        IDataHandler dateHandler;


        /// <summary>
        /// API-токен бота телеграм
        /// </summary>
        private const string Token = "6694819520:AAGeqWeAEHH3m7kJ4jphSq6IjOrWU_zSh64";

        /// <summary>
        /// Объект клиента бота Телеграм
        /// </summary>
        private readonly ITelegramBotClient _bot = new TelegramBotClient(Token);

        public TelegramBot(IHabitService habitService, ICaretaker caretaker, IOriginator originator, IReciever reciever, IDataHandler nameHandler, IDataHandler typeHandler, IDataHandler descHandler, IDataHandler dateHandler)
        {
            this.habitService = habitService;

            this.originator = originator;
            this.caretaker = caretaker;

            this.reciever = reciever;
            this.nameHandler = nameHandler;
            this.typeHandler = typeHandler;
            this.descHandler = descHandler;
            this.dateHandler = dateHandler;

            this.nameHandler.AppointSuccesor(this.typeHandler);
            this.typeHandler.AppointSuccesor(this.descHandler);
            this.descHandler.AppointSuccesor(this.dateHandler);

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
                if (caretaker.GetUserState(username) == null)
                {
                    caretaker.AddUserState(username, currentState);
                }

                originator.SetMemento(caretaker.GetUserState(username));
                var receivedState = originator.GetMemento();
                
                if(message.Text.StartsWith("/"))         
                {

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


                }
                else if (receivedState == "/создать")
                {
                    //самый важный момент //Начало цепочки 
                    //Видимо, тут надо сделать await, так как исходя из тестов с точкой останова, метод Handle не успевает сработать.
                    nameHandler.Handle(reciever, habitService, message.Text);




                    if (!reciever.GetTypeExistence())
                    {
                        await botClient.SendTextMessageAsync(message.Chat, $"Название привычки успешно записано)");

                        await botClient.SendTextMessageAsync(message.Chat, $"Теперь введите её тип (хорошая/плохая).");

                    }
                    else if (!reciever.GetDescExistence())
                    {
                        await botClient.SendTextMessageAsync(message.Chat, $"Тип привычки успешно записан)");

                        await botClient.SendTextMessageAsync(message.Chat, $"А теперь введите её описание");
                    }
                    else if (!reciever.GetDateExistence())
                    {
                        await botClient.SendTextMessageAsync(message.Chat, $"Описание привычки успешно записано)");

                        await botClient.SendTextMessageAsync(message.Chat, $"По каким дням или датам вы хотите получать напоминания ?");
                    }
                    else if (reciever.GetDateExistence())
                    {
                        caretaker.RemoveUserState(username);
                        caretaker.AddUserState(username, currentState);
                        await botClient.SendTextMessageAsync(message.Chat, $"Прекрасно. Привычка создана!");
                    }

                }
                else if(receivedState == "/изменить")
                {
                    //логика изменения уже созданной привычки
                }
                else if( receivedState== "/привычки")
                {
                    var existingHabit = habitService.GetInfo();
                    await botClient.SendTextMessageAsync(message.Chat, $"Ваша привычка:\n{existingHabit}");

                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"Привет-привет, {username} :)\nВведи команду, используя \"/\"");
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

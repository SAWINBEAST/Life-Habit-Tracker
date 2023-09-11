using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Newtonsoft.Json;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTrackerConsole
{
    /// <inheritdoc cref="IBot"/>.
    internal class TelegramBot : IBot
    {
        //Объекты внешних классов логики программы
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

        public TelegramBot(IHabitService habitService, ICaretaker caretaker, IOriginator originator, IReciever reciever, INameHandler nameHandler, ITypeHandler typeHandler, IDescHandler descHandler, IDateHandler dateHandler)
        {
            this.habitService = habitService;

            this.originator = originator;
            this.caretaker = caretaker;

            this.reciever = reciever;
            this.nameHandler = (IDataHandler)nameHandler;
            this.typeHandler = (IDataHandler)typeHandler;
            this.descHandler = (IDataHandler)descHandler;
            this.dateHandler = (IDataHandler)dateHandler;

            //Создание Цепочки обязанностей. Соединение звеньев
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
            //Проверка типа обновления от бота
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                //создаем новые переменные от частоиспользуемых свойств обновления
                var message = update.Message;
                var username = message.From.Username;

                Console.WriteLine($"Cooбщение от пользователя {username}: {message?.Text}");

                //Фиксируем текущее состояние работы клиента с ботом. При необходимости записываем его в Смотрителя(хранителя)
                var currentState = originator.CreateMemento(message.Text);
                if (caretaker.GetUserState(username) == null)
                    caretaker.AddUserState(username, currentState);
                

                //Достаем из хранителя актуальное состояние работы с ботом, если в текущий момент ведется работа по определённой команде,
                //которую ранее, скорее всего, не записали, так как состояние уже существовало. 
                originator.SetMemento(caretaker.GetUserState(username));
                var receivedState = originator.GetMemento();

                //При ввода команды со слешем, нам нужно начать работу с новой командой,
                //перезаписать состояние работы с ботом в хрпнителе
                //И правильно ответить на запрос клиента
                if (message.Text.StartsWith("/"))
                {
                    switch (message?.Text?.ToLower())
                    {
                        case "/start":
                            await botClient.SendTextMessageAsync(message.Chat, $"Добро пожаловать в Привычковную,{username}");

                            caretaker.RemoveUserState(username);
                            //вот тут прописать логику выпадания меню. См. заметки в ТГ
                            break;

                        case "/создать":
                            await botClient.SendTextMessageAsync(message.Chat, $"Сейчас создадим");
                            await botClient.SendTextMessageAsync(message.Chat, $"Введите название Привычки");

                            caretaker.RemoveUserState(username);
                            caretaker.AddUserState(username, currentState);
                            break;

                        case "/привычки":
                            await botClient.SendTextMessageAsync(message.Chat, $"Ваша привычка: Какую привычку вы хотите посмотреть ?");

                            caretaker.RemoveUserState(username);
                            caretaker.AddUserState(username, currentState);
                            break;

                            //Остальные команды :
                    }
                    return;
                }
                //вывод дефолтного сообщения в чат, на случай неправильного вода клиента
                else if (receivedState == null) 
                {
                    await botClient.SendTextMessageAsync(message.Chat, $"Привет-привет, {username} :)\nВведи команду, используя \"/\"");
                }


                //Работа с простым текстом и данными для определенной команды.
                //Определить логику работы помогает текущее состояние работы с ботом, записанное ранее.
                switch (receivedState)
                {
                    case "/создать":
                        //Начало цепочки 
                        await nameHandler.Handle(reciever, habitService, message.Text);

                        //Дерево Ифов, так как в зависимости от успешной записи данных клиентом
                        //нужно вывести ответ в чат, и запросить ввод новый данных.
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
                            await botClient.SendTextMessageAsync(message.Chat, $"Прекрасно. Привычка создана!");
                        }

                        break;

                    case "/привычки":
                        //тут будет работа с БД и выгрузка нужной привычки
                        //а пока что, выгрузка только что введённой привычки из оперативной памяти
                        var existingHabit = habitService.GetInfo();
                        await botClient.SendTextMessageAsync(message.Chat, $"Ваша привычка:\n{existingHabit}");

                        caretaker.RemoveUserState(username);
                        break;

                   
                        //Остальные состояния и логика работа в них :
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
            Console.WriteLine($"Произошла ошибка:\n{JsonConvert.SerializeObject(exception)}");
            await botClient.SendTextMessageAsync(botClient.BotId, "В нашей работе произошла ошибка. Мы уже решаем её");

        }

        /// <inheritdoc/>
        public async Task LaunchAsync()
        {
            var botName = await _bot.GetMeAsync();
            Console.WriteLine("Запущен бот " + botName.FirstName);

            //Подготовка нужных параметров для метода Запуска Бота
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };


            //Начало получения обновлений от бота
            _bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }
        
    }
}

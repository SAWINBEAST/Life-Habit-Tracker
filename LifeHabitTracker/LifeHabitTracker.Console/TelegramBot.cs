using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Newtonsoft.Json;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IChain;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IHabit;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IState;
using LifeHabitTracker.BusinessLogicLayer.Impls.General;

namespace LifeHabitTrackerConsole
{
    /// <inheritdoc cref="IBot"/>.
    public class TelegramBot : IBot
    {
        //Объекты внешних классов логики программы

        IOriginator originator;
        ICaretaker caretaker;
        IContextHabitCreation context;

        /// <summary>
        /// API-токен бота телеграм
        /// </summary>
        private const string Token = "6694819520:AAGeqWeAEHH3m7kJ4jphSq6IjOrWU_zSh64";

        /// <summary>
        /// Объект клиента бота Телеграм
        /// </summary>
        private readonly ITelegramBotClient _bot = new TelegramBotClient(Token);

        public TelegramBot(ICaretaker caretaker, IOriginator originator, IContextHabitCreation context )
        {
            this.originator = originator;
            this.caretaker = caretaker;
            this.context = context;

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

                //Узнаем, была ли у нас до этого момента команда
                string? previousState = null; 

                if (message.Text.StartsWith("/"))   //это можно убрать внутрь метода addUserState
                {
                    previousState = caretaker.GetUserState(username);
                    caretaker.RemoveUserState(username);
                    caretaker.AddUserState(username, currentState);

                }    

                //Достаем из хранителя актуальное состояние работы с ботом, если в текущий момент ведется работа по определённой команде,
                //которую ранее, скорее всего, не записали, так как состояние уже существовало. 
                originator.SetMemento(caretaker.GetUserState(username));
                var receivedState = originator.GetMemento();


                if (receivedState != null)
                {
                    if (previousState != null)  //на последнем этапе его нужно удалить
                        await botClient.SendTextMessageAsync(message.Chat, $"Произошел сброс команды, начинаем заново.");

                    switch (receivedState)
                    {
                        case Constant.StartCommand:

                            await botClient.SendTextMessageAsync(message.Chat, $"Добро пожаловать в Привычковную,{username}");
                            caretaker.RemoveUserState(username);

                            //вот тут прописать логику выпадания меню. См. заметки в ТГ
                            break;

                        case Constant.CreateCommand:

                            await botClient.SendTextMessageAsync(message.Chat, context.RequestWriteValue(message.Text));

                            context.RequestNextState();

                            //ВАЖНО
                            //после завершения записи привычки мне нужно удалить состояние.
                            //В классах состояния у меня нет доступа к объекту caretaker.я не знаю, как мне его удалить.
                            //Это важно, потому что контекст создания привычки у меня остается и после завершения создания привычки.
                            break;

                        case Constant.GetHabitCommand:
                            await botClient.SendTextMessageAsync(message.Chat, $"- {context.GetUsedHabit().Name} -\n- {context.GetUsedHabit().Desc} -");

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

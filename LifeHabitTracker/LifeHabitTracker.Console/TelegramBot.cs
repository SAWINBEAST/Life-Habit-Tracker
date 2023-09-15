using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTrackerConsole.Entities;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace LifeHabitTrackerConsole
{
    /// <inheritdoc cref="IBot"/>.
    public class TelegramBot : IBot
    {
        /// <summary>
        /// API-токен бота телеграм
        /// </summary>
        private const string Token = "6694819520:AAGeqWeAEHH3m7kJ4jphSq6IjOrWU_zSh64";

        /// <summary>
        /// Объект клиента бота Телеграм
        /// </summary>
        private readonly ITelegramBotClient _bot = new TelegramBotClient(Token);

        /// <summary>
        /// Хранитель контекстов
        /// </summary>
        private readonly IHabitContextCaretaker _habitContextCaretaker;

        /// <summary>
        /// Сервис привычек
        /// </summary>
        private readonly IHabitService _habitService;

        public TelegramBot(IHabitContextCaretaker habitContextCaretaker, IHabitService habitService)
        {
            _habitContextCaretaker = habitContextCaretaker;
            _habitService = habitService;
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

                if (string.IsNullOrEmpty(message?.Text))
                    return;

                var messageText = message.Text;

                Console.WriteLine($"Cooбщение от пользователя {username}: {messageText}");

                //Узнаем, была ли у нас до этого момента команда
                var context = _habitContextCaretaker.GetContext(username); 

                if (messageText.StartsWith("/") && context is not null)   //это можно убрать внутрь метода addUserState
                {
                    _habitContextCaretaker.RemoveContext(username);
                    await botClient.SendTextMessageAsync(message.Chat, "Вы вышли из процесса.");
                }

                switch (messageText)
                {
                    case Command.Start:
                        await HandleStartCommandAsync(message.Chat, username);
                        //вот тут прописать логику выпадания меню. См. заметки в ТГ
                        break;
                    case Command.CreateHabit:
                        await HandleCreateHabitCommandAsync(message.Chat, username, cancellationToken);
                        break;
                    case Command.Habits:
                        var habitNames = _habitService.GetHabits().Select(x => x.Name);
                        await botClient.SendTextMessageAsync(message.Chat, $"Привычки:\n{string.Join("\n", habitNames)}");
                        break;
                    default:
                        if (context is not null)
                        {
                            await context.HandleUserResponseAsync(messageText, cancellationToken);
                            return;
                        }
                        await botClient.SendTextMessageAsync(message.Chat, "Команда не распознана.");
                        break;
                }
            }
        }

        /// <summary>
        /// Обработать команду старта
        /// </summary>
        /// <param name="chat">Информация по чату</param>
        /// <param name="username">Имя пользователя</param>
        /// <returns></returns>
        private async Task HandleStartCommandAsync(Chat chat, string username)
            => await _bot.SendTextMessageAsync(chat, $"Добро пожаловать в Привычковную, {username}");

        /// <summary>
        /// Обработать команду по созданию привычки
        /// </summary>
        /// <param name="chat">Информация по чату</param>
        /// <param name="username">Имя пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        private async Task HandleCreateHabitCommandAsync(Chat chat, string username, CancellationToken cancellationToken)
        {
            var chatInfo = new ChatInfo(chat.Id, username);
            var context = _habitContextCaretaker.CreateContext(chatInfo, HandleHabitCreationInfoAsync);
            await context.StartContextAsync(cancellationToken);
        }

        /// <summary>
        /// Обработать информацию по созданию привычки
        /// </summary>
        /// <param name="chatInfo">Информация по чату</param>
        /// <param name="message">Информационное сообщение</param>
        /// <param name="isFinish">Признак того, что процесс создания завершён (true)</param>
        /// <param name="habit">Данные по создаваемой привычке</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        private async Task HandleHabitCreationInfoAsync(ChatInfo chatInfo, string message, bool isFinish, Habit habit, CancellationToken cancellationToken)
        {
            var messageInfo = await _bot.SendTextMessageAsync(chatInfo.ChatId, message, cancellationToken: cancellationToken);
            if (isFinish)
            {
                await _bot.SendTextMessageAsync(chatInfo.ChatId, 
                    _habitService.AddHabit(habit)
                        ? "Привычка успешно добавлена!"
                        : "Не удалось добавить привычку", 
                    replyToMessageId: messageInfo.MessageId, 
                    cancellationToken: cancellationToken);
                _habitContextCaretaker.RemoveContext(chatInfo.UserName);
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
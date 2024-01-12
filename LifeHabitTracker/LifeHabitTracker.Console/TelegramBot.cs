﻿using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTrackerConsole.Entities;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace LifeHabitTrackerConsole
{
    /// <inheritdoc cref="ITelegramBot"/>.
    public class TelegramBot : ITelegramBot
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
        private readonly IContextCaretaker _habitContextCaretaker;

        /// <summary>
        /// Сервис привычек
        /// </summary>
        private readonly IHabitService _habitService;

        public TelegramBot(IContextCaretaker habitContextCaretaker, IHabitService habitService)
        {
            _habitContextCaretaker = habitContextCaretaker;
            _habitService = habitService;
        }

        /// <summary>
        /// Логика выдачи ответа Бота на введённую команду + Логирование введённой команды
        /// </summary>
        /// <param name="botClient">Ссылка на телеграмм-бота </param>
        /// <param name="update">Информация о полученном сообщении и его отправителе</param>
        /// <param name="cancellationToken">Токен для отмены выполнения операции</param>
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                var username = message.From.Username;

                if (string.IsNullOrEmpty(message?.Text))
                    return;

                var messageText = message.Text;

                Console.WriteLine($"Cooбщение от пользователя {username}: {messageText}");

                var context = _habitContextCaretaker.GetContext(username); 

                if (messageText.StartsWith("/") && context is not null)   
                {
                    _habitContextCaretaker.RemoveContext(username);
                    await botClient.SendTextMessageAsync(message.Chat, "Вы вышли из процесса.");
                }

                switch (messageText)
                {
                    case Command.Start:
                        await HandleCommandStartAsync(message.Chat, username);
                        //TODO: прописать логику выпадания меню. Для красоты. См. заметки в ТГ
                        break;
                    case Command.CreateHabit:
                        await HandleCommandCreateHabitAsync(message.Chat, username, cancellationToken);
                        break;
                    case Command.Habits:
                        await HandleCommandViewHabitsAsync(message.Chat, cancellationToken);
                        break;
                    case Command.CertainHabit:
                        await HandleCommandViewCertainHabitAsync(message.Chat, username, cancellationToken);
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
        private async Task HandleCommandStartAsync(Chat chat, string username)
            => await _bot.SendTextMessageAsync(chat, $"Добро пожаловать в Привычковную, {username}");       //TODO: Подробная инструкция по работе с ботом 

        /// <summary>
        /// Обработать команду по созданию привычки <see cref="Habit"/>
        /// </summary>
        /// <param name="chat">Информация по чату</param>
        /// <param name="username">Имя пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        private async Task HandleCommandCreateHabitAsync(Chat chat, string username, CancellationToken cancellationToken)
        {
            var chatInfo = new ChatInfo(chat.Id, username);
            var context = _habitContextCaretaker.MakeHabitCreationContext(chatInfo, HandleHabitCreationInfoAsync);
            await context.StartContextAsync(cancellationToken);
        }

        /// <summary>
        /// Обработать команду по выдаче всех привычек клиента
        /// </summary>
        /// <param name="chat">Информация по чату</param>
        /// <param name="cancellationToken">Токен отмены</param>
        private async Task HandleCommandViewHabitsAsync(Chat chat, CancellationToken cancellationToken)
        {
            var habits = await _habitService.GetHabitsAsync(chat.Id);
            var message = new StringBuilder();

            if (habits != null && habits.Count > 0)
            {
                message.Append("Ваши Привычки:\n");
                foreach (var habit in habits)
                {
                    message.Append(@"\n- {habit.Name} - 
                                \n- {habit.Type} привычка - 
                                \n- Что делать: {habit.Description} -\n");
                }
            }
            else message.Append("Вы еще не завели привычки.\nВоспользуйтесь командой \\createHabit и заведите новую привычку :)");

            await _bot.SendTextMessageAsync(chat, 
                message.ToString(),
                replyToMessageId: (int)chat.Id,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Обработать команду по выводу информации о конкретной привычке
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task HandleCommandViewCertainHabitAsync(Chat chat, string username, CancellationToken cancellationToken)
        {
            var chatInfo = new ChatInfo(chat.Id, username);
            var context = _habitContextCaretaker.MakeCertainHabitInfoContext(chatInfo, HandleCertainHabitReadingAsync);
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
        private async Task HandleHabitCreationInfoAsync(ChatInfo chatInfo, string message, bool isFinish, Habit habit, CancellationToken cancellationToken)
        {
            var messageInfo = await _bot.SendTextMessageAsync(chatInfo.ChatId, message, cancellationToken: cancellationToken);
            if (isFinish)
            {
                await _bot.SendTextMessageAsync(chatInfo.ChatId,
                    await _habitService.AddHabitAsync(habit, chatInfo.ChatId)
                        ? "Привычка успешно добавлена!"
                        : "Не удалось добавить привычку",
                    replyToMessageId: messageInfo.MessageId,
                    cancellationToken: cancellationToken);

                _habitContextCaretaker.RemoveContext(chatInfo.UserName);
            }
        }

        /// <summary>
        /// Обработать информацию по чтению информации о привычке
        /// </summary>
        /// <param name="chatInfo">Информация по чату</param>
        /// <param name="message">Информационное сообщение</param>
        /// <param name="isRecieved">Признак того, что привычка получена</param>
        /// <param name="habit">Данные по создаваемой привычке</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        private async Task HandleCertainHabitReadingAsync(ChatInfo chatInfo, string requestedHabit, CancellationToken cancellationToken)
        {
            /*            var messageInfo = await _bot.SendTextMessageAsync(chatInfo.ChatId, message, cancellationToken: cancellationToken);
                        if (!isRecieved)
                        {
                            _habitService.GetCertainHabit()
                        }
                        else
                        {
                            if (habit != null)
                            {*/

            var recievedHabit = await _habitService.GetCertainHabitAsync(chatInfo.ChatId, requestedHabit);
            if (recievedHabit != null)
            {
                    await _bot.SendTextMessageAsync(chatInfo.ChatId,
                            @$"\n- {recievedHabit.Name} - 
                            \n- {recievedHabit.Type} привычка - 
                            \n- Что делать: {recievedHabit.Description} -\n
                            \n- Когда напомнить: {recievedHabit.Date} -\n",
                            replyToMessageId: (int)chatInfo.ChatId,
                            cancellationToken: cancellationToken);
            }
            else await _bot.SendTextMessageAsync(chatInfo.ChatId, "\nВы еще не завели такую привычку");

                //_habitContextCaretaker.RemoveContext(chatInfo.UserName);
            
        }

        /// <summary>
        /// Логирование ошибки при взаимодействии пользователя с ботом
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(botClient.BotId, "В нашей работе произошла ошибка. Мы уже решаем её");
            Console.WriteLine($"Произошла ошибка:\n{JsonConvert.SerializeObject(exception)}");
            //TODO: Сделать, чтобы сообщение клиенту отправлялось. Сейчас не отправляется. Попробовать try catch
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


            _bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }
    }
}
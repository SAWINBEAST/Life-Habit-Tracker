using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    internal class ContextCertainHabitReading : IContext
    {
        /// <summary>
        /// Выводимая пользователю привычка
        /// </summary>
        //private IHabitService _habitService;

        /// <summary>
        /// Данные чата, в рамках которого существует контекст
        /// </summary>
        public ChatInfo ChatInfo { get; }

        const string DataRequestMessage = "Отлично!\nКакую привычку вы бы хотели посмотреть?\nВведите её Название:";

        /// <summary>
        /// Ссылка на обработчика запроса со стороны сервера, который будет вызываться после обработки полученных от пользователя данных
        /// </summary>
        private event Func<ChatInfo,string, bool, CancellationToken, Task> DataCompleted;

        public ContextCertainHabitReading(ChatInfo chatInfo, Func<ChatInfo, string, bool, CancellationToken, Task> handleRequestFunc)
        {
            //_habitService = new HabitService();
            ChatInfo = chatInfo;
            DataCompleted += handleRequestFunc;
        }

        /// <inheritdoc/>
        public async Task StartContextAsync(CancellationToken cancellationToken)
            => await DataCompleted(ChatInfo, DataRequestMessage, false, cancellationToken);

        /// <inheritdoc/>
        public async Task HandleUserResponseAsync(string userResponse, CancellationToken cancellationToken)
        {
            await DataCompleted(ChatInfo, userResponse, true, cancellationToken);
        }
        ///TODO:Мне не нравится, что StartContextAsync и HandleUserResponseAsync почти что одинаковые. Наверное, надо их как-то объединить, меняя в зависимости от ситуации 1 и 2 параметры


    }
}

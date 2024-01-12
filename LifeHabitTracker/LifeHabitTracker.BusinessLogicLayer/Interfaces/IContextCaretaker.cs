using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;
using System;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Хранитель контекстов команд пользователя (для дальнейших действий по этим командам)
    /// </summary>
    public interface IContextCaretaker
    {
        /// <summary>
        /// Добавление нового состояния создания привычки определенного юзера в словарь
        /// </summary>
        /// <param name="chatInfo">Данные чата, в рамках которого будет существовать контекст</param>
        /// <param name="handleRequestFunc">Ссылка на обработчка запроса со стороны сервера, который будет вызываться после обработки полученных от пользователя данных</param>
        /// <returns>Созданный контекст</returns>
        public IContext MakeHabitCreationContext(ChatInfo chatInfo, Func<ChatInfo, string, bool, Habit, CancellationToken, Task> handleRequestFunc);

        /// <summary>
        /// Добавление нового состояния чтения инфы определённой привычки определенного юзера в словарь
        /// </summary>
        /// <param name="chatInfo"></param>
        /// <returns></returns>
        public IContext MakeCertainHabitInfoContext(ChatInfo chatInfo, Func<ChatInfo, string, CancellationToken, Task> handleRequestFunc);

        /// <summary>
        /// Получить текущий контекст пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Текущий контекст пользователя (если такой существует)</returns>
        public IContext? GetContext(string username);

        /// <summary>
        /// Удалить контекст пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        public void RemoveContext(string username);
    }
}
using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits
{
    /// <summary>
    /// Хранитель контекстов по созданию привычки
    /// </summary>
    public interface IHabitContextCaretaker
    {
        /// <summary>
        /// Добавление нового состояния определенного юзера в словарь
        /// </summary>
        /// <param name="chatInfo">Данные чата, в рамках которого будет существовать контекст</param>
        /// <param name="handleRequestFunc">Ссылка на обработчка запроса со стороны сервера, который будет вызываться после обработки полученных от пользователя данных</param>
        /// <returns>Созданный контекст</returns>
        public IContextHabitCreation CreateContext(ChatInfo chatInfo, Func<ChatInfo, string, bool, Habit, CancellationToken, Task> handleRequestFunc);

        /// <summary>
        /// Получить текущий контекст пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Текущий контекст пользователя (если такой существует)</returns>
        public IContextHabitCreation? GetContext(string username);

        /// <summary>
        /// Удалить контекст пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        public void RemoveContext(string username);
    }
}
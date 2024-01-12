using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="IContextCaretaker"/>
    internal class ContextsCaretaker : IContextCaretaker
    {
        /// <summary>
        /// Словарь контекстов по созданию привычки по пользователям
        /// </summary>
        private readonly IDictionary<string, IContext> _usersHabitContexts = new Dictionary<string, IContext>();

        /// <summary>
        /// Cписок контекстов клиентов, которые хотят получить инфу по определённой привычке
        /// </summary>
        private readonly IDictionary<string, IContext> _userCertainHabitContexts = new Dictionary<string, IContext>();


        /// <inheritdoc/>
        public IContext MakeHabitCreationContext(ChatInfo chatInfo, Func<ChatInfo, string, bool, Habit, CancellationToken, Task> handleRequestFunc)
        {
            var newContext = new ContextHabitCreation(chatInfo, handleRequestFunc);
            _usersHabitContexts.Add(chatInfo.UserName, newContext);
            Console.WriteLine($"Для {chatInfo.UserName} создан контекст процесса создания привычки. Идентификатор чата, в рамках которого существует контекст: {chatInfo.ChatId}.");
            return newContext;
        }

        /// <inheritdoc/>
        public IContext MakeCertainHabitInfoContext(ChatInfo chatInfo, Func<ChatInfo, string, CancellationToken, Task> handleRequestFunc) 
        {
            var newContext = new ContextCertainHabitReading(chatInfo, handleRequestFunc);
            _userCertainHabitContexts.Add(chatInfo.UserName, newContext);
            Console.WriteLine($"Для {chatInfo.UserName} создан контекст процесса чтения информации о конкретной привычке. Идентификатор чата, в рамках которого существует контекст: {chatInfo.ChatId}.");
            return newContext;
        }
        /// <inheritdoc/>
        public IContext? GetContext(string username)
            => _usersHabitContexts.ContainsKey(username)
                ? _usersHabitContexts[username]
                : _userCertainHabitContexts.ContainsKey(username) ? _userCertainHabitContexts[username] : null;

        /// <inheritdoc/>
        public void RemoveContext(string username)
        {
            if (_usersHabitContexts.ContainsKey(username))
            {
                _usersHabitContexts.Remove(username);
                Console.WriteLine($"Удалён контекст процесса создания привычки для пользователя {username}.");
            }
        }
    }
}
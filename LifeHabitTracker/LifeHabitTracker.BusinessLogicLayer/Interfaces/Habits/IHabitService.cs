using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits
{
    /// <summary>
    /// Описывает функционал работы с Привычкой
    /// </summary>
    public interface IHabitService
    {
        /// <summary>
        /// Добавить привычку
        /// </summary>
        /// <param name="habit">Добавляемая привычка</param>
        /// <param name="chatId">ID чата, из которого добавляют привычку</param>
        /// <returns>True - привычка успешно добавлена, False - привычку не удалось добавить</returns>
        public Task<bool> AddHabitAsync(Habit habit, long chatId);

        /// <summary>
        /// Получить все привычки клиента
        /// </summary>
        /// <param name="chatId">ID чата, из которого запрашивают все привычки</param>
        /// <returns>Привычки пользователя</returns>
        public Task<IReadOnlyCollection<Habit>> GetHabitsAsync(long chatId);

        /// <summary>
        /// Получить определённую привычку клиента
        /// </summary>
        /// <param name="requestedHabit">Название запрашиваемой привычки</param>
        /// <param name="chatId">ID чата, из которого запрашивают определённую привычку</param>
        /// <returns></returns>
        public Task<Habit> GetCertainHabitAsync(long chatId ,string requestedHabit);
    }
}

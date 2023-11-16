using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits
{
    /// <summary>
    /// Описывает функционал Привычки
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
        /// Получить привычки
        /// </summary>
        /// <returns>Привычки</returns>
        public IReadOnlyCollection<Habit> GetHabits();

    }
}

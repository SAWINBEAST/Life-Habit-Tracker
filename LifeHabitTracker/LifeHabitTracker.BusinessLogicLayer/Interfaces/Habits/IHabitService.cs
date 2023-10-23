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
        /// <returns>True - привычка успешно добавлена, False - привычку не удалось добавить</returns>
        public Task<bool> AddHabitAsync(Habit habit, long chatId);

        /// <summary>
        /// Получить привычки
        /// </summary>
        /// <returns>Привычки</returns>
        public IReadOnlyCollection<Habit> GetHabits();

        /// <summary>
        /// Взятие привычки для её дальнейшего использования
        /// </summary>
        /// <param name="habit"></param>
        public void TakeHabit(Habit habit);

        /// <summary>
        /// Рассказывает о привычке. Что она из себя представляет, какие есть составляющие
        /// </summary>
        string GetInfo();

    }
}

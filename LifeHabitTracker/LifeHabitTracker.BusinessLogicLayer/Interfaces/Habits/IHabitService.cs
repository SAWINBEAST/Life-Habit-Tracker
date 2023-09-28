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
        public bool AddHabit(Habit habit);

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

        /*
        /// <summary>
        /// Загрузка имени
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name);

        /// <summary>
        /// Загрузка типа
        /// </summary>
        /// <param name="name"></param>
        public void SetType(string type);

        /// <summary>
        /// Загрузка описания
        /// </summary>
        /// <param name="name"></param>
        public void SetDesc(string desc);

        /// <summary>
        /// Загрузка даты
        /// </summary>
        /// <param name="name"></param>
        public void SetDate(string date);
*/
    }
}

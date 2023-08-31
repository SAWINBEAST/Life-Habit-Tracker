
namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Описывает функционал Создателя и Управляющего Привычек ("HabitFather")
    /// </summary>
    internal interface IHabitCreator
    {
        /// <summary>
        /// Создание новой сущности привычки
        /// </summary>
        /// <returns></returns>
        Habit CreateHabit();

        /// <summary>
        /// Удалить существующую сущность привычки
        /// </summary>
        /// <returns></returns>
        Task DeleteHabit();

        /// <summary>
        /// Обновить данные уже созданной привычки
        /// </summary>
        /// <returns></returns>
        Task UpdateHabit();
    }
}

using LifeHabitTracker.BusinessLogicLayer.Entities;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <summary>
    /// Сущность привычки
    /// </summary>
    public class Habit
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Даты выполнения привычки
        /// </summary>
        public ReminderDate Date { get; set; }

    }
}
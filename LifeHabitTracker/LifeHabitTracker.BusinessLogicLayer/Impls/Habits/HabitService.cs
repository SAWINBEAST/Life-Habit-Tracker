using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <inheritdoc cref="IHabitService"/>.
    public class HabitService : IHabitService
    {
        /// <summary>
        /// Текущая привычка для дальнейшей обработки
        /// </summary>
        public Habit CurrentHabit = new Habit();

        /// <inheritdoc/>
        public void  TakeHabit(Habit habit)
        {
            CurrentHabit = habit;
        }

        /// <inheritdoc/>
        public string GetInfo()
        {
            var info = $"-{CurrentHabit.Name}-\n-{CurrentHabit.Description}-\n-{CurrentHabit.Type}-\n-{CurrentHabit.Date}-";
            return info;
        }

        /// <inheritdoc/>
        public bool AddHabit(Habit habit) => true;

        /// <inheritdoc/>
        public IReadOnlyCollection<Habit> GetHabits() => new Habit[] { new Habit() };

    }
}

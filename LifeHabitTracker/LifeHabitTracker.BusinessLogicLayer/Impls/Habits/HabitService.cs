using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <inheritdoc cref="IHabitService"/>.
    public class HabitService : IHabitService
    {
            public Habit CurrentHabit = new Habit();
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
        /// временная заглушка. Потом тут будет добавление в БД
        public bool AddHabit(Habit habit) => true;

        /// <inheritdoc/>
        public IReadOnlyCollection<Habit> GetHabits() => new Habit[] { new Habit() };

/*
        /// <inheritdoc/>
        public void SetName(string name)
        {
            CurrentHabit.Name = name;
        }

        /// <inheritdoc/>
        public void SetType(string type)
        {
            CurrentHabit.Type = type;
        }

        /// <inheritdoc/>
        public void SetDesc(string desc)
        {
            CurrentHabit.Description = desc;
        }

        /// <inheritdoc/>
        public void SetDate(string date)
        {
            CurrentHabit.Date = date;
        }
*/
    }
}

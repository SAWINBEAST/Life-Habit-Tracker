using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.IHabit;

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
            CurrentHabit.Desc = desc;
        }

        /// <inheritdoc/>
        public void SetDate(string date)
        {
            CurrentHabit.Date = date;
        }

        /// <inheritdoc/>
        public string GetInfo()
        {
            var info = $"-{CurrentHabit.Name}-\n-{CurrentHabit.Desc}-\n-{CurrentHabit.Type}-\n-{CurrentHabit.Date}-";
            return info;
        }

    }
}

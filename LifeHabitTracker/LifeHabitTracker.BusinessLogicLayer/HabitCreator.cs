using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IHabitCreator"/>.
    public class HabitCreator:IHabitCreator
    {
        /// <inheritdoc/>
        public Habit CreateHabit()  //потом добавить параметры
        {
            var habit = new Habit();    //потом использовать конструктор с параметрами
            return habit;
        }

        /// <inheritdoc/>
        public Task DeleteHabit()
        {
            //Временная заглушка
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task UpdateHabit()
        {
            //Временная заглушка
            return Task.CompletedTask;
        }

    }
}

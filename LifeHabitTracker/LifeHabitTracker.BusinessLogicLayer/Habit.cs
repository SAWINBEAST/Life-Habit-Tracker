using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IHabit"/>.
    public class Habit:IHabit
    {

        public string Name { get; set; } = "Какая-то Привычка";

        public string Description { get; set; } = "Нужно делать то-то то-то раз в день";

        public string Type { get; set; } = "Хорошая";

        public DateTime Date { get; set; } = DateTime.Now.AddDays(1);

        public Habit() {
            Console.WriteLine("Создана тестовая привычка");
        }

        public Habit(string name, string desc, string type, DateTime date ) {
            Name = name;
            Description = desc;
            Type = type;
            Date = date;
        }

        /// <inheritdoc/>
        public List<object> GetInfo()
        {
            var info = new List<object>() { Name, Description, Type, Date};
            return info;
        }

        /// <inheritdoc/>
        public Task RemindAbout()
        {
            //Временная заглушка
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task CommitExecution()
        {
            //Временная заглушка
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task GetConclusion()
        {
            //Временная заглушка
            return Task.CompletedTask;
        }


    }
}

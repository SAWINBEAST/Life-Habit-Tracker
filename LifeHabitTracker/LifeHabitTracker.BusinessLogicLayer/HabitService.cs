using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IHabitService"/>.
    public class HabitService:IHabitService
    {
        //Это дичь какая-то. Надо разделить HabitService и Habit. Понятное дело, что потом у меня Привычки будут храниться в БД, Но временно же надо что-то сделать, чтобы программа хоть что-то выдавала.
        public List<HabitService> habits  = new List<HabitService>();   //я понимаю, что это всё не сохранится. Просто хочу, чтобы программа что-то выдавала
        public string Name { get; set; } = "Какая-то Привычка";

        public string Description { get; set; } = "Нужно делать то-то то-то раз в день";

        public string Type { get; set; } = "Хорошая";

        public DateTime Date { get; set; } = DateTime.Now.AddDays(1);

        public HabitService() {
            Console.WriteLine("Создана тестовая привычка");
        }

        public HabitService(string name, string desc, string type, DateTime date ) {
            Name = name;
            Description = desc;
            Type = type;
            Date = date;
        }

        /// <inheritdoc/>
        public void CreateHabit(string name, string desc, string type, DateTime date)
        {
            var habit = new HabitService(name, desc, type, date);
            habits.Add(habit);

            Console.WriteLine($"Создана привычка \"{name}\"");
        }

        /// <inheritdoc/>
        public List<object> GetInfo()
        {
            var info = new List<object>() {Name, Description, Type, Date};
            return info;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="IHabitService"/>.
    public class HabitService : IHabitService
    {
        //Это дичь какая-то. Надо разделить HabitService и Habit. Понятное дело, что потом у меня Привычки будут храниться в БД, Но временно же надо что-то сделать, чтобы программа хоть что-то выдавала.
        public List<HabitService> habits = new List<HabitService>();   //я понимаю, что это всё не сохранится. Просто хочу, чтобы программа что-то выдавала
        public string Name { get; set; } = "Какая-то Привычка";
        public string Type { get; set; } = "Хорошая";
        public string Description { get; set; } = "Нужно делать то-то то-то раз в день";
        public string Date { get; set; } = "понедельник";
        //public DateTime Date { get; set; } = DateTime.Now.AddDays(1);

        public HabitService()
        {
        }

        /// <inheritdoc/>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <inheritdoc/>
        public void SetType(string type)
        {
            Type = type;
        }

        /// <inheritdoc/>
        public void SetDesc(string desc)
        {
            Description = desc;
        }

        /// <inheritdoc/>
        public void SetDate(string date)
        {
            Date = date;
        }

        /// <inheritdoc/>
        public string GetInfo()
        {
            var info = $"-{Name}-\n-{Description}-\n-{Type}-\n-{Date}-" ;
            return info;
        }

    }
}

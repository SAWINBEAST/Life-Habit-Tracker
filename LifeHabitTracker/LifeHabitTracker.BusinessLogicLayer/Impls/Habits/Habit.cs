using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    public class Habit
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public string Date { get; set; }
        //public DateTime Date { get; set; } = DateTime.Now.AddDays(1);



    }
}

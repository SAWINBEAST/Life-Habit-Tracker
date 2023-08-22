using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_Habit_Tracker
{
    internal class SomeHabit : IHabitCreationInterface
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public SomeHabit(string name) 
        {
            Name = name;
            //Id = id;
            //Description = descript;
        }

        public void Create()
        {
            Console.WriteLine("Привычка создана");
        }
    }
}

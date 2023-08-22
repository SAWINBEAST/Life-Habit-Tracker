using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_Habit_Tracker
{
    internal class Client : HabitExecutionInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<SomeHabit> someHabits = new List<SomeHabit>();

        public Client(int id, string name) 
        {
            Id = id;
            Name = name;
            Console.WriteLine($"Новый пользователь {name}");
        }

        public string MakeHabit(string name)
        {
            SomeHabit firstHabit = new SomeHabit(name);
            someHabits.Add(firstHabit);
            firstHabit.Create();

            return "Привычка введена в жизнь";        
        }

        public void Execute()
        {
            foreach(SomeHabit habbit in someHabits) 
            {
                Console.WriteLine($"Привычка {habbit.Name} выполнена");
                Thread.Sleep(1000);

            }

        }

    }
}

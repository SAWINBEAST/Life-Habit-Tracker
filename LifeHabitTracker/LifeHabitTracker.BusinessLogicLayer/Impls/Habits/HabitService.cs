using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;


namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <inheritdoc cref="IHabitService"/>.
    public class HabitService : IHabitService
    {
        /// <summary>
        /// Текущая привычка для дальнейшей обработки
        /// </summary>
        public Habit CurrentHabit = new Habit();


        private readonly IDataManage _dataManager;

        private readonly Dictionary<string, string> _daysTypesOfNames = new Dictionary<string, string>()
        {
            {"пн", "monday"},
            {"понедельник", "monday"},
            {"вт", "tuesday"},
            {"вторник", "tuesday"},
            {"ср", "wednesday"},
            {"среда", "wednesday"},
            {"чт", "thursday"},
            {"четверг", "thursday"},
            {"пт", "friday"},
            {"пятница", "friday"},
            {"сб", "saturday"},
            {"суббота", "saturday"},
            {"вс", "sunday"},
            {"воскресенье", "sunday"}
        };

        public HabitService(IDataManage dataManager)
        {
            _dataManager = dataManager;
        }
       

        /// <inheritdoc/>
        public async Task<bool> AddHabitAsync(Habit habit, long chatId)
        {
            var name = habit.Name;
            var desc = habit.Description;
            var type = () => (habit.Type == "хорошая") ? 1 : 0;
            var isGood = type();
            IReadOnlyCollection<string> days = null;
            IReadOnlyCollection<string> times = null;

            if (isGood== 1)
            {
                days = habit.Date.day;
                times = habit.Date.time;
            }
            
            var daysAndReminds = new Dictionary<string, int>()
            {
                {"monday", 0 },
                {"tuesday", 0 },
                {"wednesday", 0 },
                {"thursday", 0 },
                {"friday", 0 },
                {"saturday", 0 },
                {"sunday", 0 }
            };

            foreach(var day in days)
            {
                if (_daysTypesOfNames.ContainsKey(day))
                {
                    daysAndReminds[_daysTypesOfNames[day]] = 1;
                }
                else if (day == DayOfWeekInfo.Weekdays)
                {
                    daysAndReminds.Clear();
                    daysAndReminds = new Dictionary<string, int>()
                    {
                        {"monday", 1 },
                        {"tuesday", 1 },
                        {"wednesday", 1 },
                        {"thursday", 1 },
                        {"friday", 1 },
                        {"saturday", 0 },
                        {"sunday", 0 }
                    };
                }
                else if (day == DayOfWeekInfo.Weekend) 
                {
                    daysAndReminds["saturday"]= 1;
                    daysAndReminds["sunday"] = 1;
                }
                else
                {
                    daysAndReminds.Clear();
                    daysAndReminds = new Dictionary<string, int> {
                        { "monday", 1 },
                        { "tuesday", 1 },
                        { "wednesday", 1 },
                        { "thursday", 1 },
                        { "friday", 1 },
                        { "saturday", 1 },
                        { "sunday", 1 }
                    };
                }
            }


            return _dataManager.WriteHabitInfo(name, desc, chatId, isGood, daysAndReminds, times);




        } 







        /// <inheritdoc/>
        public IReadOnlyCollection<Habit> GetHabits() => new Habit[] { new Habit() };

        /// <inheritdoc/>
        public void TakeHabit(Habit habit)
        {
            CurrentHabit = habit;
        }

        /// <inheritdoc/>
        public string GetInfo()
        {
            var info = $"-{CurrentHabit.Name}-\n-{CurrentHabit.Description}-\n-{CurrentHabit.Type}-\n-{CurrentHabit.Date}-";
            return info;
        }
    }
}


/*var times = "";
foreach (var time in habit.Date.time)
{
    if (((IEnumerator<string>)habit.Date.time).MoveNext())
    {
        times = times + time + ",";

    }
    times = times + time;
}*/

using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Interfaces;

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
        public bool AddHabit(Habit habit)
        {
            var name = habit.Name;
            var desc = habit.Description;
            var type = () => (habit.Type == "хорошая") ? 1 : 0;
            var times = habit.Date.time;
            var days = habit.Date.day;

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
                if(_daysTypesOfNames.ContainsKey(day));
            }






            return true;
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

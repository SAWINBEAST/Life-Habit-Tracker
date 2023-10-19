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

        public HabitService(IDataManage dataManager)
        {
            _dataManager = dataManager;
        }
       

        /// <inheritdoc/>
        public bool AddHabit(Habit habit)
        {
            //здесь Обработка данных привычки под тип БД





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

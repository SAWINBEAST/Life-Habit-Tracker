using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;
using LifeHabitTracker.BusinessLogicLayer.Entities;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{
    public class DateState : HabitCreationState
    {
        public DateState() => DataRequestMessage = "Ввведите Дату или Дни напоминания о привычке.";

        DayOfWeekInfo _dayOfWeek = new DayOfWeekInfo();

        /// <inheritdoc/>
        public override (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {
            var day = _dayOfWeek.FindDay(data);

            if (day != "такого дня нет")
            {
                habit.Date = data;

                Console.WriteLine($"Введённые данные для Даты напоминания о привычке: {data}");

                return ($"Дата привычки: {data}.", true);
            }

            return ($"{day}. Введите день заново.", false);
        }

        /// <inheritdoc/>
        protected override IHabitCreationState TransitionToNewState() => null;
    }
}

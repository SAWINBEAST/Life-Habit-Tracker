using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Interfaces;


namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <inheritdoc cref="IHabitService"/>.
    internal class HabitService : IHabitService
    {
        /// <summary>
        /// Объект сервиса добавления привычки в БД
        /// </summary>
        private IInsertHabitService _insertHabitService;

        public HabitService(IInsertHabitService insertHabitService)
        {
            _insertHabitService = insertHabitService;
        }

        /// <inheritdoc/>
        public async Task<bool> AddHabitAsync(Habit habit, long chatId)
        {
            var habitsTableData = PrepareHabitsData(habit, chatId);
            var daysTableData = PrepareDaysData(habit);
            var timesTableData = PrepareTimesData(habit);
            return await _insertHabitService.InsertHabitAsync(habitsTableData, daysTableData, timesTableData);
        }

        /// <summary>
        /// Подготовка данных к записи в таблицу habits.
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнес-логики </param>
        /// <param name="chatId">ID чата, из которого добавляют привычку</param>
        /// <returns> Сущность основных данных о привычке </returns>
        private static DbHabits PrepareHabitsData(Habit habit, long chatId)
            => new()
            {
                Name = habit.Name,
                Description = habit.Description,
                ChatId = chatId,
                IsGood = habit.Type == FundamentalConcept.Good
            };

        /// <summary>
        /// Подготовка данных к записи в таблицу days
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнес-логики </param>
        /// <returns> Сущность данных о днях </returns>
        private static DbDays PrepareDaysData(Habit habit)
            => habit.Date is not null && habit.Date.Days.Any()
            ? new()
            {
                OnMonday = habit.Date.Days.Contains(RussianDays.Monday),
                OnTuesday = habit.Date.Days.Contains(RussianDays.Tuesday),
                OnWednesday = habit.Date.Days.Contains(RussianDays.Wednesday),
                OnThursday = habit.Date.Days.Contains(RussianDays.Thursday),
                OnFriday = habit.Date.Days.Contains(RussianDays.Friday),
                OnSaturday = habit.Date.Days.Contains(RussianDays.Saturday),
                OnSunday = habit.Date.Days.Contains(RussianDays.Sunday)
            }
            : null;

        /// <summary>
        /// Подготовка данных для записи в таблицу times
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнес-логики </param>
        /// <returns> Сущность данных о времени </returns>
        private static DbTimes PrepareTimesData(Habit habit)
            => habit.Date is not null && habit.Date.Times.Any()
            ? new() { Times = habit.Date.Times }
            : null;

        /// <inheritdoc/>
        public IReadOnlyCollection<Habit> GetHabits() => new Habit[] { new Habit() };
    }
}
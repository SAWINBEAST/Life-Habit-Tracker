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
        private IDBHabitProvider _dbHabitProvider;

        public HabitService(IDBHabitProvider insertHabitService)
        {
            _dbHabitProvider = insertHabitService;
        }

        /// <inheritdoc/>
        public async Task<bool> AddHabitAsync(Habit habit, long chatId)
        {
            var habitsTableData = PrepareHabitsData(habit, chatId);
            var daysTableData = PrepareDaysData(habit);
            var timesTableData = PrepareTimesData(habit);
            return await _dbHabitProvider.InsertHabitAsync(habitsTableData, daysTableData, timesTableData);
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
            ? new() { Times = (ICollection<string>)habit.Date.Times }
            : null;

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Habit>> GetHabitsAsync(long chatId)
        {
            var selectedHabits = await _dbHabitProvider.SelectHabitsInfoAsync(chatId);
            return selectedHabits != null ? PrepareClientHabits(selectedHabits) : null;
        }

        /// <summary>
        /// Подготовка вида данных для выдачи 
        /// </summary>
        /// <param name="selectedHabits">Привычки вида Уровня данных</param>
        /// <returns>Коллекция привычек пользовательского вида</returns>
        private static IReadOnlyCollection<Habit> PrepareClientHabits(IReadOnlyCollection<DbHabits> selectedHabits)
            => selectedHabits.Select(x => new Habit
                                        { Name = x.Name, 
                                        Description = x.Description, 
                                        Type = x.IsGood == true ? "Хорошая" : "Плохая" })
                             .ToArray();

        ///<inheritdoc/>
        public async Task<Habit> GetCertainHabitAsync(long chatId, string requestedHabit)
        {
            var selectedHabit = await _dbHabitProvider.SelectCertainHabitInfoAsync(chatId, requestedHabit.ToLower());
            return selectedHabit.Item1 != null ? PrepareCertainHabit(selectedHabit) : null;
        }

        /// <summary>
        /// Подготовка данных Привычки к виду клиента
        /// </summary>
        /// <param name="selectedHabit">Кортеж данных о привычке вида БД</param>
        /// <returns>Привычка вида клиента</returns>
        private static Habit PrepareCertainHabit((DbHabits, DbDays, DbTimes) selectedHabit)
            => new()
            {
                Name = selectedHabit.Item1.Name,
                Description = selectedHabit.Item1.Description,
                Type = selectedHabit.Item1.IsGood ? FundamentalConcept.Good : FundamentalConcept.Bad,
                Date = PrepareHabitDate(selectedHabit.Item2, selectedHabit.Item3)
            };

        /// <summary>
        /// Подготовка данных Даты и Времени напоминания о привычке к виду клиента
        /// </summary>
        /// <param name="dbDays">Дни вида БД</param>
        /// <param name="dbTimes">Время вида БД</param>
        /// <returns>Полная запись даты для напоминания</returns>
        private static ReminderDate PrepareHabitDate(DbDays dbDays, DbTimes dbTimes)
        {
            var days = new List<string>();
            //TODO: тернарный оператор отказывается выполнять days.Add() и выполнять пропуск хода, хотя должен. Разобраться в этом.
            if (dbDays.OnMonday) days.Add(RussianDays.MondayFull);
            if (dbDays.OnTuesday) days.Add(RussianDays.TuesdayFull);
            if (dbDays.OnWednesday) days.Add(RussianDays.WednesdayFull);
            if (dbDays.OnThursday) days.Add(RussianDays.ThursdayFull);
            if (dbDays.OnFriday) days.Add(RussianDays.FridayFull);
            if (dbDays.OnSaturday) days.Add(RussianDays.SaturdayFull);
            if (dbDays.OnSunday) days.Add(RussianDays.SundayFull);


            return new(days, (IReadOnlyCollection<string>)dbTimes.Times);
                
            
        }
           
        
    }
}
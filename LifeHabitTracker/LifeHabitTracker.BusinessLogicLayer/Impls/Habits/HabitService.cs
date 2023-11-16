using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTracker.Entities.PreparedData;
using LifeHabitTracker.Entities;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <inheritdoc cref="IHabitService"/>.
    internal class HabitService : IHabitService
    {
        /// <summary>
        /// Объект сервиса добавления привычки в БД
        /// </summary>
        private IInsertHabitService _habitInserter;

        /// <summary>
        /// Объединение одинаковых названий дней
        /// </summary>
        private readonly Dictionary<string, string> _daysTypesOfNames = new Dictionary<string, string>()
        {
            {RussianDays.Monday, EnglishDays.Monday},
            {RussianDays.MondayFull, EnglishDays.Monday},

            {RussianDays.Tuesday, EnglishDays.Tuesday},
            {RussianDays.TuesdayFull, EnglishDays.Tuesday},

            {RussianDays.Wednesday, EnglishDays.Wednesday},
            {RussianDays.WednesdayFull, EnglishDays.Wednesday},

            {RussianDays.Thursday, EnglishDays.Thursday},
            {RussianDays.ThursdayFull, EnglishDays.Thursday},

            {RussianDays.Friday, EnglishDays.Friday},
            {RussianDays.FridayFull, EnglishDays.Friday},

            {RussianDays.Saturday, EnglishDays.Saturday},
            {RussianDays.SaturdayFull, EnglishDays.Saturday},

            {RussianDays.Sunday, EnglishDays.Sunday},
            {RussianDays.SundayFull, EnglishDays.Sunday},

        };

        public HabitService(IInsertHabitService habitInserter)
        {
            _habitInserter = habitInserter;
        }


        /// <inheritdoc/>
        public async Task<bool> AddHabitAsync(Habit habit, long chatId)
        {
            var habitsTableData = PrepareHabitsData(habit, chatId);
            var daysTableData = PrepareDaysData(habit, habitsTableData.IsGood);
            var timesTableData = PrepareTimesData(habit, habitsTableData.IsGood);

            return await _habitInserter.InsertHabitAsync(habitsTableData, daysTableData, timesTableData);

        }

        /// <summary>
        /// Подготовка данных к записи в таблицу habits.
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнес-логики </param>
        /// <param name="chatId">ID чата, из которого добавляют привычку</param>
        /// <returns> Сущность основных данных о привычке </returns>
        private DbHabits PrepareHabitsData(Habit habit, long chatId)
        {
            var habitsTableData = new DbHabits();

            habitsTableData.Name = habit.Name;
            habitsTableData.Description = habit.Description;
            habitsTableData.ChatId = chatId;
            habitsTableData.IsGood = habit.Type == FundamentalConcept.Good;

            return habitsTableData;
        }


        /// <summary>
        /// Подготовка данных к записи в таблицу days
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнес-логики </param>
        /// <param name="isGood"> Тип привычки, его булевая интерпретация </param>
        /// <returns> Сущность данных о днях </returns>
        private DbDays PrepareDaysData (Habit habit, bool isGood)
        {
            var daysTableData = new DbDays();

            if (isGood)
            {
                foreach (var day in habit.Date.day)
                {
                    if (_daysTypesOfNames.ContainsKey(day))
                    {
                        daysTableData.DaysAndReminds[_daysTypesOfNames[day]] = 1;
                    }
                    else if (day == RussianDays.Weekdays)
                    {
                        daysTableData.DaysAndReminds.Clear();
                        daysTableData.DaysAndReminds = new Dictionary<string, int>()
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
                    else if (day == RussianDays.Weekend)
                    {
                        daysTableData.DaysAndReminds["saturday"] = 1;
                        daysTableData.DaysAndReminds["sunday"] = 1;
                    }
                    else
                    {
                        daysTableData.DaysAndReminds.Clear();
                        daysTableData.DaysAndReminds = new Dictionary<string, int>
                        {
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
            }

            return daysTableData;

        }

        /// <summary>
        /// Подготовка данных для записи в таблицу times
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнес-логики </param>
        /// <param name="isGood"> Тип привычки, его булевая интерпретация </param>
        /// <returns> Сущность данных о времени </returns>
        private DbTimes PrepareTimesData (Habit habit, bool isGood)
        {
            var timesTableData = new DbTimes ();

            if (isGood)
            {
                timesTableData.Times = habit.Date.time;
            }

            return timesTableData;
        }


        /// <inheritdoc/>
        public IReadOnlyCollection<Habit> GetHabits() => new Habit[] { new Habit() };

    }
}
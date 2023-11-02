using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <inheritdoc cref="IHabitService"/>.
    public class HabitService : IHabitService
    {
        /// <summary>
        /// Объект сервиса добавления привычки в БД
        /// </summary>
        private IInsertHabitService _habitInserter;

        /// <summary>
        /// Информация о подключаемой БД
        /// </summary>
        public static DataBaseConnect _dBConfig;

        /// <summary>
        /// Объект подготовленных данных для таблицы habits
        /// </summary>
        private PreparedHabitsTableData _habitsTableData = new PreparedHabitsTableData();

        /// <summary>
        /// Объект подготовленных данных для таблицы days
        /// </summary>
        private PreparedDaysTableData _daysTableData = new PreparedDaysTableData();

        /// <summary>
        /// Объект подготовленных данных для таблицы times
        /// </summary>
        private PreparedTimesTableData _timesTableData = new PreparedTimesTableData();

        /// <summary>
        /// Объединение одинаковых названий дней
        /// </summary>
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

        public HabitService(DataBaseConnect dBConfig, IInsertHabitService habitInserter)
        {
            _dBConfig = dBConfig;
            _habitInserter = habitInserter;
        }


        /// <inheritdoc/>
        public async Task<bool> AddHabitAsync(Habit habit, long chatId)
        {
            var dbName = _dBConfig.DBName;

            PrepareDataForTable(habit, chatId);

            return await _habitInserter.InsertHabitAsync(_habitsTableData, _daysTableData, _timesTableData, dbName);

        }

        /// <summary>
        /// Подготовка данных к записи в таблицу.
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнесЛогики </param>
        /// <param name="chatId">ID чата, из которого добавляют привычку</param>
        private void PrepareDataForTable(Habit habit, long chatId)
        {

            _habitsTableData.Name = habit.Name;
            _habitsTableData.Description = habit.Description;
            _habitsTableData.ChatId = chatId;

            var type = () => (habit.Type == "хорошая") ? 1 : 0;
            _habitsTableData.IsGood = Convert.ToBoolean(type());


            if (_habitsTableData.IsGood)
            {
                _timesTableData.Times = habit.Date.time;

                foreach (var day in habit.Date.day)
                {
                    if (_daysTypesOfNames.ContainsKey(day))
                    {
                        _daysTableData.DaysAndReminds[_daysTypesOfNames[day]] = 1;
                    }
                    else if (day == DayOfWeekInfo.Weekdays)
                    {
                        _daysTableData.DaysAndReminds.Clear();
                        _daysTableData.DaysAndReminds = new Dictionary<string, int>()
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
                        _daysTableData.DaysAndReminds["saturday"] = 1;
                        _daysTableData.DaysAndReminds["sunday"] = 1;
                    }
                    else
                    {
                        _daysTableData.DaysAndReminds.Clear();
                        _daysTableData.DaysAndReminds = new Dictionary<string, int>
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

        }


        /// <inheritdoc/>
        public IReadOnlyCollection<Habit> GetHabits() => new Habit[] { new Habit() };

    }
}
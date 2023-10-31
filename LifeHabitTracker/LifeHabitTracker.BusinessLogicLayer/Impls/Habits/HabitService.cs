using LifeHabitTracker.BusinessLogicLayer.Interfaces.Habits;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.DataAccessLayer.Entities;
using System.Transactions;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.Habits
{
    /// <inheritdoc cref="IHabitService"/>.
    public class HabitService : IHabitService
    {
        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу days
        /// </summary>
        private readonly IHabitsTableRepository _habitsManager;

        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу habits
        /// </summary>
        private readonly IDaysTableRepository _daysManager;

        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу times
        /// </summary>
        private readonly ITimesTableRepository _timesManager;

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

        public HabitService(IHabitsTableRepository habitsManager, IDaysTableRepository daysManager, ITimesTableRepository timesManager, DataBaseConnect dBConfig)
        {
            _habitsManager = habitsManager;
            _daysManager = daysManager;
            _timesManager = timesManager;
            _dBConfig = dBConfig;
        }


        /// <inheritdoc/>
        public async Task<bool> AddHabitAsync(Habit habit, long chatId)
        {
            var dbName = _dBConfig.DBName;

            PrepareDataForTable(habit);

            //TODO: Сделать работу в одной транзакции. На уровне DataAccess
            var habitId = await _habitsManager.InsertIntoHabitsTableAsync(_habitsTableData, dbName);

            if (_habitsTableData.IsGood)
            {
                var resultOfDaysInsert = await _daysManager.InsertIntoDaysTableAsync(_daysTableData, dbName, habitId);
                var resultOfTimesInsert = await _timesManager.InsertIntoTimesTableAsync(_timesTableData, dbName, habitId);

                if (resultOfDaysInsert == true && resultOfTimesInsert == true)
                {
                    return true;
                }
                else return false;

            }
            else if (habitId != null) return true;
            else return false;

        }

        /// <summary>
        /// Подготовка данных к записи в таблицу.
        /// </summary>
        /// <param name="habit"> Привычка уровня бизнесЛогики </param>
        private void PrepareDataForTable(Habit habit)
        {

            _habitsTableData.Name = habit.Name;
            _habitsTableData.Description = habit.Description;

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
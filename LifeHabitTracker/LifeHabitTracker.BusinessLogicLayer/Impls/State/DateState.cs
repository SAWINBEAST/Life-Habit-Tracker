using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;
using LifeHabitTracker.BusinessLogicLayer.Entities;
using LifeHabitTracker.Entities;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{    
    /// <summary>
    /// Состояние получения даты напоминания привычки
    /// </summary>
    internal class DateState : HabitCreationState
    {
        /// <summary>
        /// Шаблоны названий дней для напоминания
        /// </summary>
        private readonly IList<string> _dayTemplates = new List<string>
        {
            RussianDays.Monday, RussianDays.MondayFull,
            RussianDays.Tuesday, RussianDays.TuesdayFull,
            RussianDays.Wednesday, RussianDays.WednesdayFull,
            RussianDays.Thursday, RussianDays.ThursdayFull,
            RussianDays.Friday, RussianDays.FridayFull,
            RussianDays.Saturday, RussianDays.SaturdayFull,
            RussianDays.Sunday, RussianDays.SundayFull,
            RussianDays.Weekdays, RussianDays.Weekend,
            RussianDays.Daily, RussianDays.Everyday
        };


        public DateState() => DataRequestMessage = "\nВвведите Дни недели (через запятую) и Время напоминания о привычке." +
                                                    "\nСначала введите Дни, используя ключевое слово \"Дни:\"." +
                                                    "\nПосле введите Время, используя ключевое слово \"Время:\"." +
                                                    "\nВы также можете выбрать \"Будни\",\"Выходные\",\"Ежедневно\"." +
                                                    "\n\nНапример:" +
                                                    "\nДни:Пн,Ср,Пт" +
                                                    "\nВремя:18:00" +
                                                    "\n\nИли:" +
                                                    "\nДни:Будни" +
                                                    "\nВремя:8:00,12:00,18:00";

        /// <inheritdoc/>
        public override (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit)
        {
            const string patternDay = "дни:";
            const string patternTime = "время:";

            Console.WriteLine($"Введённые данные для Даты напоминания о привычке: {data}");

            var daysAndTime = data.Replace(" ",string.Empty)
                                  .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (daysAndTime[0] != null && daysAndTime[1] != null)
            {

                if (!TryParseDays(daysAndTime[0].ToLower().Replace(patternDay, string.Empty), out var days))
                    return ($"Вы ввели дни в Некорректной форме." +
                            $"\nПроверьте раннее введённые данные и Сравните их с шаблоном: Пн,Ср,Пт" +
                            $"\nВы также можете выбрать \"Будни\",\"Выходные\",\"Ежедневно\"." +
                            $"\nВведите дни и время заново, пожалуйста :)", false);


                if (!TryParseTimes(daysAndTime[1].ToLower().Replace(patternTime, string.Empty), out var times))
                    return ("Проверьте корректность введённого времени.\nНапоминаем, что время нужно ввести по шаблону.\nНапример 18:38 или 9:00", false);

                habit.Date = new ReminderDate(days, times);
                return ($"Дни напоминания о привычке: {string.Join(',', days)}.\nВремя напоминания о привычке: {string.Join(',', times)}", true);
            }

            return ("Похоже, что вы забыли ввести время напоминания. " +
                        "Либо же дни недели. Пожалуйста, перепроверьте введённые данные и сравните их с шаблоном", false);
        }

        /// <summary>
        /// Проверка корректности введённых дней
        /// </summary>
        /// <param name="resultDay">Перечисление дней через запятую</param>
        /// <param name="days">Отредактированный массив дней недели</param>
        /// <returns>Результат проверки корректности введённых дней</returns>
        private bool TryParseDays(string resultDay, out IReadOnlyCollection<string> days)
        {
            days = resultDay.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            days = days.Distinct().ToArray();
            foreach (var day in days)
                if (!_dayTemplates.Contains(day))
                    return false;
          
            return true;
        }

        /// <summary>
        /// Проверка корректности введённого времени
        /// </summary>
        /// <param name="resultTime">Перечисление времени через запятую</param>
        /// <param name="times">Отредактированный массив времени</param>
        /// <returns>Результат проверки корректности введённого времени</returns>
        private static bool TryParseTimes(string resultTime, out IReadOnlyCollection<string> times)
        {
            times = resultTime.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            times = times.Distinct().ToArray();
            foreach (var time in times)
                if (!DateTime.TryParse(time, out _))
                    return false;
            return true;
        }

        /// <inheritdoc/>
        protected override IHabitCreationState TransitionToNewState() => throw new NotImplementedException("Продолжение Не реализовано. Это последнее звено.");
    }
}



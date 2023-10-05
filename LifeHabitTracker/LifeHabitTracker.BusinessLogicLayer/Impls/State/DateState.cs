﻿using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;
using LifeHabitTracker.BusinessLogicLayer.Entities;
using System.Text.RegularExpressions;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{    
    /// <summary>
    /// Состояние получения даты напоминания привычки
    /// </summary>
    public class DateState : HabitCreationState
    {
        /// <summary>
        /// Шаблоны названий дней для напоминания
        /// </summary>
        private readonly IList<string> _dayTemplates = new List<string>
        {
            DayOfWeekInfo.Monday, DayOfWeekInfo.MondayFull, 
            DayOfWeekInfo.Tuesday, DayOfWeekInfo.TuesdayFull, 
            DayOfWeekInfo.Wednesday, DayOfWeekInfo.WednesdayFull, 
            DayOfWeekInfo.Thursday, DayOfWeekInfo.ThursdayFull, 
            DayOfWeekInfo.Friday, DayOfWeekInfo.FridayFull, 
            DayOfWeekInfo.Saturday, DayOfWeekInfo.SaturdayFull, 
            DayOfWeekInfo.Sunday, DayOfWeekInfo.SundayFull, 
            DayOfWeekInfo.Weekdays, DayOfWeekInfo.Weekend, 
            DayOfWeekInfo.Daily, DayOfWeekInfo.Everyday 
        };

        //меганепрактичный вариант, мне кажется. Но проще для записи. Есть подробная инструкция.
        public DateState() => DataRequestMessage = "\nВвведите Дни недели (через запятую) и Время напоминания о привычке." +
                                                    "\nСначала введите Дни, используя ключевое слово \"Дни:\"." +
                                                    "\nПосле введите Время, используя ключевое слово \"Часы:\"." +
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
            // Регулярное выражение для проверки введённой даты
            const string patternDay = "дни:";
            const string patternTime = "время:";

            Console.WriteLine($"Введённые данные для Даты напоминания о привычке: {data}");

            var daysAndTime = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (daysAndTime[0] != null && daysAndTime[1] != null)
            {
                // Проверка Дней
                if (!TryParseDays(daysAndTime[0].ToLower().Replace(patternDay, string.Empty), out var days))
                    return ($"Вы ввели дни в Некорректной форме." +
                            $"\nПроверьте раннее введённые данные и Сравните их с шаблоном: Пн,Ср,Пт" +
                            $"\nВы также можете выбрать \"Будни\",\"Выходные\",\"Ежедневно\"." +
                            $"\nВведите дни и время заново, пожалуйста :)", false);

                // Проверка времени
                if (!TryParseTimes(daysAndTime[1].ToLower().Replace(patternTime, string.Empty), out var times))
                    return ("Проверьте корректность введённого времени.\nНапоминаем, что время нужно ввести по шаблону.\nНапример 18:38 или 9:00", false);

                habit.Date = new ReminderDate(days, times);
                return ($"Дни напоминания о привычке: {string.Join(',', days)}.\nВремя напоминания о привычке: {string.Join(',', times)}", true);
            }

            return ("Похоже, что вы не забыли ввести время напоминания. " +
                        "Либо же дни недели. Пожалуйста, перепроверьте введённые данные и сравните их с шаблоном", false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultDay"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private bool TryParseDays(string resultDay, out IReadOnlyCollection<string> days)
        {
            days = resultDay.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            days = days.Distinct().ToArray();
            foreach (var day in days)
                if (!_dayTemplates.Contains(day.ToLower()))
                    return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultTime"></param>
        /// <param name="times"></param>
        /// <returns></returns>
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
        protected override IHabitCreationState TransitionToNewState() => throw new NotImplementedException("Не реализовано");
    }
}
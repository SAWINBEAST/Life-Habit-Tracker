using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;
using LifeHabitTracker.BusinessLogicLayer.Interfaces.State;
using LifeHabitTracker.BusinessLogicLayer.Entities;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace LifeHabitTracker.BusinessLogicLayer.Impls.State
{    
     /// <summary>
     /// Состояние получения даты напоминания привычки
     /// </summary>
    public class DateState : HabitCreationState
    {
        /// <summary>
        /// Объект дней и времени
        /// </summary>
        ReminderDate fullDate;

        /// <summary>
        /// Шаблоны названий дней для напоминания
        /// </summary>
        readonly List<string> dayTemplates = new List<string>(){DayOfWeekInfo.Monday, DayOfWeekInfo.MondayFull, 
                                                                DayOfWeekInfo.Tuesday, DayOfWeekInfo.TuesdayFull, 
                                                                DayOfWeekInfo.Wednesday, DayOfWeekInfo.WednesdayFull, 
                                                                DayOfWeekInfo.Thursday, DayOfWeekInfo.ThursdayFull, 
                                                                DayOfWeekInfo.Friday, DayOfWeekInfo.FridayFull, 
                                                                DayOfWeekInfo.Saturday, DayOfWeekInfo.SaturdayFull, 
                                                                DayOfWeekInfo.Sunday, DayOfWeekInfo.SundayFull, 
                                                                DayOfWeekInfo.Weekdays, DayOfWeekInfo.Weekend, 
                                                                DayOfWeekInfo.Daily, DayOfWeekInfo.Everyday };



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
            /// Переменные для парсинга и проверки данных и времени напоминания
            string[] daysAndTime;
            string[] days;
            string[] times;
            string[] hoursAndMins;
            string resultDay;
            string resultTime;


            /// Регулярное выражение для проверки введённой даты
            /// ?? ^
            string patternDay = @"^дни:";
            string patternTime = @"^время:";
            string targetEmpty = "";

            Regex regexDay = new Regex(patternDay);
            Regex regexTime = new Regex(patternTime);


            // RegexOptions.IgnorePatternWhitespace  //RegexOptions.Multiline //RegexOptions.IgnoreCase
           

            daysAndTime = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                
            if (daysAndTime[0] != null && daysAndTime[1] != null)
            {
                ///Проверка Дней
                resultDay = regexDay.Replace(daysAndTime[0].ToLower(), targetEmpty);

                //тут может произойти ошибка
                days = resultDay.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for(int i = 0; i < days.Length; i++)    //(string day in days)
                {
                        if (!dayTemplates.Contains(days[i].ToLower()))
                        {
                            return ($"Вы ввели дни в Некорректной форме." +
                                    $"\nПроверьте раннее введённые данные и Сравните их с шаблоном: Пн,Ср,Пт" +
                                    $"\nВы также можете выбрать \"Будни\",\"Выходные\",\"Ежедневно\"." +
                                    $"\nВведите дни и время заново, пожалуйста :)", false);
                        }

                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (days[i] == days[j])
                            {
                                return ("Похоже, что вы ввели несколько одинаковых дней недели.\nВводите только уникальные названия дней.\nНапример: Чт,Пт,Вс", false);
                            }
                        }
                    }
                        


                }



                ///Проверка времени
                resultTime = regexTime.Replace(daysAndTime[1].ToLower(), targetEmpty);

                //тут может произойти ошибка
                times = resultTime.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string time in times)
                {
                    //тут может произойти ошибка
                    hoursAndMins = time.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);


                    if (int.TryParse(hoursAndMins[0], out int hours))
                    {
                        if (hours < 0 || hours > 23)
                        {
                            return ("Проверьте корректность введённого времени.\nТакого часа в сутках не существует.\nТакже, напоминаем, что время нужно ввести по шаблону.\nНапример 18:38 или 9:00", false);
                        }
                    }
                    else return ("Ой-ой. По-моему, вы ввели не числовое значение в часе, а что-то другое.\nПерепроверьте введённые данные.\nШаблон: 18:38 или 9:00 ", false);



                    if (int.TryParse(hoursAndMins[1], out int minutes))
                    {
                        if (minutes < 0 || minutes > 59)
                        {
                            return ("Проверьте корректность введённого времени.\nТаких минут в часе не существует.\nТакже, напоминаем, что время нужно ввести по шаблону.\nНапример 18:38 или 9:00", false);
                        }
                    }
                    else return ("Ой-ой. По-моему, вы ввели не числовое значение в минутах, а что-то другое.\nПерепроверьте введённые данные.\nШаблон: 18:38 или 9:00 ", false);

                }


            }
            else
            {
                return ("Похоже, что вы не забыли ввести время напоминания. " +
                        "Либо же дни недели. Пожалуйста, перепроверьте введённые данные и сравните их с шаблоном", false);

            }

            fullDate = new ReminderDate(days, times);

            habit.Date = fullDate;

            Console.WriteLine($"Введённые данные для Даты напоминания о привычке: {data}");

            //здесь будет лажа, скорее всего. Так что это можно будет упразднить. и вообще убрать переменную fulldate
            return ($"Дни напоминания о привычке: {resultDay}.\nВремя напоминания о привычке {resultTime}", true);

            
           
        }






        

        /// <inheritdoc/>
        protected override IHabitCreationState TransitionToNewState() => null;
    }
}

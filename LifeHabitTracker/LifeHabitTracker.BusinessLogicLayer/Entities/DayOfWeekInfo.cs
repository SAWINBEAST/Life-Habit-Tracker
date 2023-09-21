using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Entities
{
    public class DayOfWeekInfo  //неоптимально. Пытался ввести паттерн 
    {
        List<string> daysOfWeek = new List<string>()
        {
            "понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье",
        };

        List<string> mondayNames = new List<string>()
        {
            "понедельник", "пон", "пн", "1", "monday"
        };

        List<string> tuesdayNames = new List<string>()
        {
            "вторник", "втор", "вт", "2", "tuesday"
        };

        List<string> wednesdayNames = new List<string>()
        {
            "среда", "сред", "ср", "3", "wednesday"
        };

        List<string> thursdayNames = new List<string>()
        {
            "четверг", "чет", "чт", "4", "thursday"
        };

        List<string> fridayNames = new List<string>()
        {
            "пятница", "пят", "пт", "5", "friday"
        };

        List<string> saturdayNames = new List<string>()
        {
            "суббота", "суб", "сб", "6", "saturday"
        };

        List<string> sundayNames = new List<string>()
        {
            "воскресенье", "воск", "вс", "7", "sunday"
        };

        List<string> everydayNames = new List<string>()
        {
            "каждый день", "ежедневно", "все дни", "каждодневно", "повседневно", "everyday"
        };

        //потом тут можно создать листы с другими словами.

        public string FindDay (string data)
        {
            var lowData = data.ToLower();
            if (mondayNames.Contains(lowData))
            {
                return daysOfWeek[0];
            }
            else if (tuesdayNames.Contains(lowData))
            {
                return daysOfWeek[1];
            }
            else if (wednesdayNames.Contains(lowData))
            {
                return daysOfWeek[2];
            }
            else if (thursdayNames.Contains(lowData))
            {
                return daysOfWeek[3];
            }
            else if (fridayNames.Contains(lowData))
            {
                return daysOfWeek[4];
            }
            else if (saturdayNames.Contains(lowData))
            {
                return daysOfWeek[5];
            }
            else if (sundayNames.Contains(lowData))
            {
                return daysOfWeek[6];
            }
            else if (everydayNames.Contains(lowData))
            {
                return daysOfWeek[1];
            }
            else return "такого дня нет";

        }
    }
}

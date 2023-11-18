using LifeHabitTracker.BusinessLogicLayer.Entities;

namespace LifeHabitTracker.BusinessLogicLayer.Helpers;

/// <summary>
/// Помощник для работы с днями
/// </summary>
internal static class DaysHelper
{
    /// <summary>
    /// Словарь пользовательского формата дней в соответствии с системным форматом
    /// </summary>
    private static readonly IDictionary<string, string> _userFormatDays = new Dictionary<string, string>()
    {
        { RussianDays.Monday, RussianDays.Monday },
        { RussianDays.MondayFull, RussianDays.Monday },

        { RussianDays.Tuesday, RussianDays.Tuesday },
        { RussianDays.TuesdayFull, RussianDays.Tuesday },

        { RussianDays.Wednesday, RussianDays.Wednesday },
        { RussianDays.WednesdayFull, RussianDays.Wednesday },

        { RussianDays.Thursday, RussianDays.Thursday },
        { RussianDays.ThursdayFull, RussianDays.Thursday },

        { RussianDays.Friday, RussianDays.Friday },
        { RussianDays.FridayFull, RussianDays.Friday },

        { RussianDays.Saturday, RussianDays.Saturday },
        { RussianDays.SaturdayFull, RussianDays.Saturday },

        { RussianDays.Sunday, RussianDays.Sunday },
        { RussianDays.SundayFull, RussianDays.Sunday }
    };

    /// <summary>
    /// Словарь системного формата дней в соответствии с пользовательским форматом
    /// </summary>
    private static readonly IDictionary<string, string> _systemFormatDays = new Dictionary<string, string>()
    {
        { RussianDays.Monday, RussianDays.MondayFull },
        { RussianDays.Tuesday, RussianDays.TuesdayFull },
        { RussianDays.Wednesday, RussianDays.WednesdayFull },
        { RussianDays.Thursday, RussianDays.ThursdayFull },
        { RussianDays.Friday, RussianDays.FridayFull },
        { RussianDays.Saturday, RussianDays.SaturdayFull },
        { RussianDays.Sunday, RussianDays.SundayFull }
    };

    /// <summary>
    /// Конвертировать введённые пользователям дни в системный формат
    /// </summary>
    /// <param name="days">Дни</param>
    /// <returns>Системный формат дней</returns>
    public static IReadOnlyCollection<string> ConvertDaysToSystemFormat(this IReadOnlyCollection<string> days)
    {
        if (days.Count == 1)
        {
            if (days.First() == RussianDays.Daily || days.First() == RussianDays.Everyday)
                return new string[]
                {
                        RussianDays.Monday,
                        RussianDays.Tuesday,
                        RussianDays.Wednesday,
                        RussianDays.Thursday,
                        RussianDays.Friday,
                        RussianDays.Saturday,
                        RussianDays.Sunday
                };

            if (days.First() == RussianDays.Weekdays)
                return new string[]
                {
                        RussianDays.Monday,
                        RussianDays.Tuesday,
                        RussianDays.Wednesday,
                        RussianDays.Thursday,
                        RussianDays.Friday
                };

            if (days.First() == RussianDays.Weekend)
                return new string[]
                {
                        RussianDays.Saturday,
                        RussianDays.Sunday
                };
        }

        return days
            .Select(x => _userFormatDays[x])
            .Distinct()
            .ToArray();
    }

    /// <summary>
    /// Конвертировать дни в системном формате в пользовательский
    /// </summary>
    /// <param name="days">Дни</param>
    /// <returns>Пользовательский формат дней</returns>
    public static IReadOnlyCollection<string> ConvertDaysToUserFormat(this IReadOnlyCollection<string> days)
    {
        if (days.Count == 7)
            return new string[] { RussianDays.Everyday };

        if (days.Count == 5 && ItWeekdays(days))
            return new string[] { RussianDays.Weekdays };

        if (days.Count == 2 && ItWeekend(days))
            return new string[] { RussianDays.Weekend };

        return days
            .Select(x => _systemFormatDays[x])
            .ToArray();
    }

    /// <summary>
    /// Проверка на то, что введённые дни соответствуют будням
    /// </summary>
    /// <param name="days">Дни</param>
    /// <returns>Соответствуют будням или нет</returns>
    private static bool ItWeekdays(IReadOnlyCollection<string> days)
        => days
            .Intersect(new string[]
            {
                RussianDays.Monday,
                RussianDays.Tuesday,
                RussianDays.Wednesday,
                RussianDays.Thursday,
                RussianDays.Friday
            })
            .Count() == 5;

    /// <summary>
    /// Проверка на то, что введённые дни соответствуют выходным
    /// </summary>
    /// <param name="days">Дни</param>
    /// <returns>Соответствуют выходным или нет</returns>
    private static bool ItWeekend(IReadOnlyCollection<string> days)
        => days
            .Intersect(new string[]
            {
                RussianDays.Saturday,
                RussianDays.Sunday
            })
            .Count() == 2;
}
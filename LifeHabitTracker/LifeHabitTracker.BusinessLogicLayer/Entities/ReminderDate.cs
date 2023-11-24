namespace LifeHabitTracker.BusinessLogicLayer.Entities;

/// <summary>
/// Полная запись даты для напоминания  
/// </summary>
/// <param name="Days">Дни</param>
/// <param name="Times">Время</param>
public record ReminderDate(IReadOnlyCollection<string> Days, IReadOnlyCollection<string> Times);
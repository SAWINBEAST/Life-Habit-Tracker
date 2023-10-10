
namespace LifeHabitTracker.BusinessLogicLayer.Entities
{
   /// <summary>
   /// Полная запись даты для напоминания  
   /// </summary>
   /// <param name="day"></param>
   /// <param name="time"></param>
    public record ReminderDate (IReadOnlyCollection<string> day, IReadOnlyCollection<string> time);
}

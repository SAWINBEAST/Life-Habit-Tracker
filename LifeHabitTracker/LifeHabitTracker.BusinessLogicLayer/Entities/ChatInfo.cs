namespace LifeHabitTracker.BusinessLogicLayer.Entities
{
    /// <summary>
    /// Информация по чату
    /// </summary>
    /// <param name="ChatId">Идентификатор чата</param>
    /// <param name="UserName">Имя пользователя</param>
    public record ChatInfo(long ChatId, string UserName);
}
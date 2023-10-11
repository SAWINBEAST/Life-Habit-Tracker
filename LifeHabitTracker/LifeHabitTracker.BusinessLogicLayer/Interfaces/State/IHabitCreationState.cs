using LifeHabitTracker.BusinessLogicLayer.Impls.Habits;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.State
{
    /// <summary>
    /// Состояние процесса создания привычки
    /// </summary>
    public interface IHabitCreationState
    {
        /// <summary>
        /// Получить текст запрос данных на текущем состоянии 
        /// </summary>
        /// <returns>Запрос данных</returns>
        public string GetDataRequest();

        /// <summary>
        /// Обработать данные
        /// </summary>
        /// <param name="context">Контекст процесса создания привычки</param>
        /// <param name="data">Данные для обработки</param>
        /// <param name="habit">Создаваемая привычка</param>
        /// <returns>Информационное сообщение по обработанным данным<br/>Завершён ли процесс полностью (если дальше никаких данных не требуется)</returns>
        public (string infoMessage, bool isFinish) HandleData(IContextHabitCreation context, string data, Habit habit);
    }
}
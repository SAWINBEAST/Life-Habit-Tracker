
namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <summary>
    /// Описывает функционал Собирателя всей информации из БД, который делает отчеты
    /// </summary>
    internal interface IAnalyst
    {
        /// <summary>
        /// Выдает количество выполненных хороших действий (привычек) или проколов
        /// </summary>
        /// <returns></returns>
        Task GetQuantityOfExecutions();

        /// <summary>
        /// Выдает математическое отношение выполненных Хороших привычек к совершенным Плохим проколам
        /// </summary>
        /// <returns></returns>
        Task GetRatioOfGoodtoBad();

        /// <summary>
        /// Выдает количество пропущенных выполнений привычки
        /// </summary>
        /// <returns></returns>
        Task GetQuantityOfPasses();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.State
{
    public interface IContext
    {
        /// <summary>
        /// Запустить работу контекста
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        public Task StartContextAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Обработать ответ пользователя в рамках работы с привычкой
        /// </summary>
        /// <param name="userResponse">Ответ пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public Task HandleUserResponseAsync(string userResponse, CancellationToken cancellationToken);
    }
}

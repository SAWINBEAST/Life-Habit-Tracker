using LifeHabitTracker.BusinessLogicLayer.Impls.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer.Interfaces.IState
{
    //Было бы хорошо сделать не интерфейс состояния, а абстрактный класс. И вывести туда переменную Context _context и метод SetContext. Но внедрение зависимостей не позволяет нам использовать абстракцию вместо интерфейса
    public interface IState
    {
        public void SetContext(ContextHabitCreation context);

        public void HandleNextState();

        public string HandleWriteValue(string data);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    public interface IDataHandler
    {
        public void Handle(Receiver receiver);
    }
}

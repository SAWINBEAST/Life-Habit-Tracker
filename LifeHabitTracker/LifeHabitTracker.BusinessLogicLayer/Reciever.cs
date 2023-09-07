using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="IReciever"/>
    public class Reciever : IReciever
    {
        public bool NameExistence { get; set; } = false;
        public bool TypeExistence { get; set; } = false;
        public bool DescExistence { get; set; } = false;
        public bool DateExistence { get; set; } = false;

        public Reciever() { }

        public bool GetNameExistence()
        {
            return NameExistence;
        }

        public bool GetTypeExistence()
        {
            return TypeExistence;
        }

        public bool GetDescExistence()
        {
            return DescExistence;
        }

        public bool GetDateExistence()
        {
            return DateExistence;
        }

        /// <inheritdoc/>
        public void ChangeExistence(int i) 
        {
            switch (i)
            {
                case 1: NameExistence = true; 
                    break;
                case 2: TypeExistence = true;
                    break;
                case 3: DescExistence = true;   
                    break;
                case 4: DateExistence = true;   
                    break;
            }
        }   
    }
}

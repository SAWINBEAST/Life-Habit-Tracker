using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.BusinessLogicLayer
{
    /// <inheritdoc cref="ICaretaker"/>
    public class Caretaker:ICaretaker
    {
        //Я бы попробовал использовать тут List<Memento> , где у Memento 2 свойства - состояние и юзернэйм. Но тогда будет сложнее находить нужное состояние.
        public Dictionary<string, Memento> UserStates = new Dictionary<string, Memento>();
        public Dictionary<string, Memento> HabitCreateStates = new Dictionary<string, Memento>();
        /// <inheritdoc/>
        public void AddUserState(string username, Memento state)
        {
            UserStates.Add(username, state);

            foreach(var states in UserStates)
{
                Console.WriteLine($"key: {states.Key}  value: {states.Value.State}");
            }
        }

        /// <inheritdoc/>
        public string GetUserState(string username)
        {
            if (UserStates.ContainsKey(username))
                return UserStates[username].State;
            else
                return null;
        }

        /// <inheritdoc/>
        public void RemoveUserState(string username) 
        {  
            UserStates.Remove(username);

            foreach (var states in UserStates)
            {
                Console.WriteLine($"key: {states.Key}  value: {states.Value.State}");
            }
        }

    }
}

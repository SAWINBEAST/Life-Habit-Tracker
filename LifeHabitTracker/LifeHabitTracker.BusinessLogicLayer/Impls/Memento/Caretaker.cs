using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeHabitTracker.BusinessLogicLayer.Interfaces;

namespace LifeHabitTracker.BusinessLogicLayer.Impls
{
    /// <inheritdoc cref="ICaretaker"/>
    public class Caretaker : ICaretaker
    {
        public Dictionary<string, Memento> UserStates = new Dictionary<string, Memento>();

        /// <inheritdoc/>
        public void AddUserState(string username, Memento state)
        {
            UserStates.Add(username, state);

            foreach (var states in UserStates)
            {
                Console.WriteLine($"Добавлено новое состояние:\n - key: {states.Key}  value: {states.Value.State} -");
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
            if (UserStates.ContainsKey(username))
                UserStates.Remove(username);

            foreach (var states in UserStates)
            {
                Console.WriteLine($"Удалено старое состояние:\n - key: {states.Key}  value: {states.Value.State} -");
            }
        }

    }
}

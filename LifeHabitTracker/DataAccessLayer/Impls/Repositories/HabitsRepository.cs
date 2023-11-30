using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;
using System.Transactions;
using System.Xml.Linq;

namespace LifeHabitTracker.DataAccessLayer.Impls.Repositories
{
    /// <inheritdoc cref="IHabitsRepository"/>.
    internal class HabitsRepository : IHabitsRepository
    {
        /// <summary>
        /// Список привычек текущего пользователя
        /// </summary>
        private List<DbHabits> _dbHabits = new();

        /// <inheritdoc/>
        public async Task<long> InsertIntoHabitsTableAsync(DbHabits habitsTableData, SqliteConnection connection, SqliteTransaction transaction)
        {
            using var commandHabitTable = new SqliteCommand(HabitsSqlFunctions.InsertAllFields, connection);
            commandHabitTable.Transaction = transaction;

            var nameParam = new SqliteParameter("@name", habitsTableData.Name);
            var descParam = new SqliteParameter("@desc", habitsTableData.Description);
            var chatIdParam = new SqliteParameter("@chatid", habitsTableData.ChatId);
            var isGoodParam = new SqliteParameter("@isgood", habitsTableData.IsGood);

            commandHabitTable.Parameters.Add(nameParam);
            commandHabitTable.Parameters.Add(descParam);
            commandHabitTable.Parameters.Add(chatIdParam);
            commandHabitTable.Parameters.Add(isGoodParam);

            var id = await commandHabitTable.ExecuteScalarAsync();
            return (long)id;
        }

        ///<inheritdoc/>
        public async Task<IReadOnlyCollection<DbHabits>> SelectAllUserHabits (long chatId, SqliteConnection connection)
        {
            using var commandHabitTable = new SqliteCommand(HabitsSqlFunctions.SelectAllHabits, connection);
            var chatIdParam = new SqliteParameter("@chatid", chatId);  //$"{chatId}"
            commandHabitTable.Parameters.Add(chatIdParam);

            using (var reader = await commandHabitTable.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var name = reader["name"];
                        var desc = reader["desc"];
                        var isGood = reader["is_good"];

                        _dbHabits.Add(new() {
                            Name = (string)name,
                            Description = (string)desc,
                            IsGood = Convert.ToBoolean((long)isGood) });
                    }
                }
            }

            return _dbHabits; 
        }
    }
}
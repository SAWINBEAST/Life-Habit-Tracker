using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;

namespace LifeHabitTracker.DataAccessLayer.Impls.Repositories
{
    /// <inheritdoc cref="IHabitsRepository"/>.
    internal class HabitsRepository : IHabitsRepository
    {
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
    }
}
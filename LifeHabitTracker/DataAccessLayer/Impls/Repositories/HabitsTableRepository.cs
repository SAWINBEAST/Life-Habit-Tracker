using Microsoft.Data.Sqlite;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using LifeHabitTracker.DataAccessLayer.Entities.Expressions;
using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;

namespace LifeHabitTracker.DataAccessLayer.Impls.Repositories
{

    /// <inheritdoc cref="IHabitsTableRepository"/>.
    public class HabitsTableRepository : IHabitsTableRepository
    {
        /// <inheritdoc/>
        public async Task<long> InsertIntoHabitsTableAsync(PreparedHabitsTableData habitsTableData, SqliteConnection connection, SqliteTransaction transaction)
        {

            var commandHabitTable = new SqliteCommand(HabitsTableExpression.InsertAllFields, connection);
            commandHabitTable.Transaction = transaction;

            var nameParam = new SqliteParameter("@name", habitsTableData.Name);
            var descParam = new SqliteParameter("@desc", habitsTableData.Description);
            var chatIdParam = new SqliteParameter("@chatid", habitsTableData.ChatId);
            var isGoodParam = new SqliteParameter("@isgood", habitsTableData.IsGood);

            commandHabitTable.Parameters.Add(nameParam);
            commandHabitTable.Parameters.Add(descParam);
            commandHabitTable.Parameters.Add(chatIdParam);
            commandHabitTable.Parameters.Add(isGoodParam);


            return (long)commandHabitTable.ExecuteScalar();


        }
    }
}

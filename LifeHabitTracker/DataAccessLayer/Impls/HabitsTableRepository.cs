using Microsoft.Data.Sqlite;
using LifeHabitTracker.DataAccessLayer.Entities;
using LifeHabitTracker.DataAccessLayer.Interfaces;


namespace LifeHabitTracker.DataAccessLayer.Impls
{

    /// <inheritdoc cref="IHabitsTableRepository"/>.
    public class HabitsTableRepository : IHabitsTableRepository
    {

        /// <inheritdoc/>
        public async Task<long> InsertIntoHabitsTableAsync(PreparedHabitsTableData habitsTableData, string dbName)
        {

            await using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var commandHabitTable = new SqliteCommand(HabitsTableExpression.InsertAllFields, connection);

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
}

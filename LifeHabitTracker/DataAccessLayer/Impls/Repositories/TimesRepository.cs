using LifeHabitTracker.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;

namespace LifeHabitTracker.DataAccessLayer.Impls.Repositories
{

    /// <inheritdoc cref="ITimesRepository"/>.
    internal class TimesRepository : ITimesRepository
    {
        ///<inheritdoc/>
        public async Task<bool> InsertIntoTimesTableAsync(DbTimes timesTableData, long habitId, SqliteConnection connection, SqliteTransaction transaction)
        {
            var numberOfRecorders = 0;

            foreach (var time in timesTableData.Times)
            {
                using var commandTimeTable = new SqliteCommand(TimesSqlFunctions.InsertTime, connection);
                commandTimeTable.Transaction = transaction;
                var idTimeParam = new SqliteParameter("@habit_id", habitId);
                var timeParam = new SqliteParameter("@time", time);

                commandTimeTable.Parameters.Add(idTimeParam);
                commandTimeTable.Parameters.Add(timeParam);

                numberOfRecorders +=  await commandTimeTable.ExecuteNonQueryAsync();
            }

            return numberOfRecorders == timesTableData.Times.Count;



        }

    }
}

using LifeHabitTracker.DataAccessLayer.Entities.Expressions;
using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;

namespace LifeHabitTracker.DataAccessLayer.Impls.Repositories
{

    /// <inheritdoc cref="ITimesTableRepository"/>.
    public class TimesTableRepository : ITimesTableRepository
    {
        ///<inheritdoc/>
        public async Task<bool> InsertIntoTimesTableAsync(PreparedTimesTableData timesTableData, long habitId, SqliteConnection connection, SqliteTransaction transaction)
        {
            var numberOfRecorders = 0;

            foreach (var time in timesTableData.Times)
            {
                var commandTimeTable = new SqliteCommand(TimesTableExpression.InsertTime, connection);
                commandTimeTable.Transaction = transaction;
                var idTimeParam = new SqliteParameter("@habit_id", habitId);
                var timeParam = new SqliteParameter("@time", time);

                commandTimeTable.Parameters.Add(idTimeParam);
                commandTimeTable.Parameters.Add(timeParam);

                numberOfRecorders = numberOfRecorders + commandTimeTable.ExecuteNonQuery();
            }

            if (numberOfRecorders == timesTableData.Times.Count) return true;
            else return false;



        }

    }
}

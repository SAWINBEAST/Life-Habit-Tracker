using LifeHabitTracker.DataAccessLayer.Entities;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHabitTracker.DataAccessLayer.Impls
{

    /// <inheritdoc cref="ITimesTableRepository"/>.
    public class TimesTableRepository : ITimesTableRepository
    {
        ///<inheritdoc/>
        public async Task<bool> InsertIntoTimesTableAsync(PreparedTimesTableData timesTableData, string dbName, long habitId)
        {
            await using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                int numberOfRecorders = 0;

                foreach (var time in timesTableData.Times)
                {
                    var commandTimeTable = new SqliteCommand(TimesTableExpression.InsertTime, connection);
                    var idTimeParam = new SqliteParameter("@habit_id", habitId);
                    var timeParam = new SqliteParameter("@time", time);

                    commandTimeTable.Parameters.Add(idTimeParam);
                    commandTimeTable.Parameters.Add(timeParam);

                    numberOfRecorders = numberOfRecorders + commandTimeTable.ExecuteNonQuery();
                }

                if(numberOfRecorders == timesTableData.Times.Count) return true;
                else return false;

            }

        }

    }
}

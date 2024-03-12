using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;

namespace LifeHabitTracker.DataAccessLayer.Impls.Repositories
{
    /// <inheritdoc cref="ITimesRepository"/>.
    internal class TimesRepository : ITimesRepository
    {
        ///TODO: Удалить и избавиться от этого поля. Оно нужно не всегда. Но без него пока не работает
        /// <summary>
        /// Обьект времени напоминания привычки вида БД
        /// </summary>
        private DbTimes _dBTimes = new();

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

                numberOfRecorders += await commandTimeTable.ExecuteNonQueryAsync();
            }
            return numberOfRecorders == timesTableData.Times.Count;
        }

        ///<inheritdoc/>
        public async Task<DbTimes> SelectFromTimesTableAsync(int habitId, SqliteConnection connection)
        {
            using var commandHabitTable = new SqliteCommand(TimesSqlFunctions.SelectTimes, connection);
            var habitIdParam = new SqliteParameter("@habit_id", habitId); 
            commandHabitTable.Parameters.Add(habitIdParam);

            using var reader = await commandHabitTable.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                _dBTimes.Times = new List<string>();

                while (reader.Read())
                {
                    var time = (string)reader["time"];
                    _dBTimes.Times.Add(time);
                }
                return _dBTimes;
            }
            return null;
        }
    }
}
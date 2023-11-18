using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;

namespace LifeHabitTracker.DataAccessLayer.Impls.Repositories
{

    /// <inheritdoc cref="IDaysRepository"/>.
    internal class DaysRepository : IDaysRepository
    {
        /// <inheritdoc/>
        public async Task<bool> InsertIntoDaysTableAsync(DbDays daysTableData, long habitId, SqliteConnection connection, SqliteTransaction transaction)
        {
            using var commandDaysTable = new SqliteCommand(DaysSqlFunctions.InsertDays, connection);
            commandDaysTable.Transaction = transaction;

            var idDaysParam = new SqliteParameter("@habit_id", habitId);
            var mondayParam = new SqliteParameter("@monday", daysTableData.OnMonday);
            var tuesdayParam = new SqliteParameter("@tuesday", daysTableData.OnTuesday);
            var wednesdayParam = new SqliteParameter("@wednesday", daysTableData.OnWednesday);
            var thursdayParam = new SqliteParameter("@thursday", daysTableData.OnThursday);
            var fridayParam = new SqliteParameter("@friday", daysTableData.OnFriday);
            var saturdayParam = new SqliteParameter("@saturday", daysTableData.OnSaturday);
            var sundayParam = new SqliteParameter("@sunday", daysTableData.OnSunday);

            commandDaysTable.Parameters.Add(idDaysParam);
            commandDaysTable.Parameters.Add(mondayParam);
            commandDaysTable.Parameters.Add(tuesdayParam);
            commandDaysTable.Parameters.Add(wednesdayParam);
            commandDaysTable.Parameters.Add(thursdayParam);
            commandDaysTable.Parameters.Add(fridayParam);
            commandDaysTable.Parameters.Add(saturdayParam);
            commandDaysTable.Parameters.Add(sundayParam);

            if (await commandDaysTable.ExecuteNonQueryAsync() != 0)
            {
                return true;
            }
            else return false;

        }
    }
}
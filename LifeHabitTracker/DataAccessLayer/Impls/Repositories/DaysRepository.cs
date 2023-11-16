using LifeHabitTracker.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Entities.SqlFunctions;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;
using LifeHabitTracker.Entities;


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
            var mondayParam = new SqliteParameter("@monday", daysTableData.DaysAndReminds[EnglishDays.Monday]);
            var tuesdayParam = new SqliteParameter("@tuesday", daysTableData.DaysAndReminds[EnglishDays.Tuesday]);
            var wednesdayParam = new SqliteParameter("@wednesday", daysTableData.DaysAndReminds[EnglishDays.Wednesday]);
            var thursdayParam = new SqliteParameter("@thursday", daysTableData.DaysAndReminds[EnglishDays.Thursday]);
            var fridayParam = new SqliteParameter("@friday", daysTableData.DaysAndReminds[EnglishDays.Friday]);
            var saturdayParam = new SqliteParameter("@saturday", daysTableData.DaysAndReminds[EnglishDays.Saturday]);
            var sundayParam = new SqliteParameter("@sunday", daysTableData.DaysAndReminds[EnglishDays.Sunday]);

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

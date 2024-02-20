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

            return await commandDaysTable.ExecuteNonQueryAsync() != 0 ? true : false;
        }

        ///<inheritdoc/>
        public async Task<DbDays> SelectFromDaysTableAsync(int habitId, SqliteConnection connection)
        {
            using var commandDaysTable = new SqliteCommand(DaysSqlFunctions.SelectDays, connection);
            var habitIdParam = new SqliteParameter("@habit_id", habitId);
            commandDaysTable.Parameters.Add(habitIdParam);

            using var reader = await commandDaysTable.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var onMonday = reader["on_monday"];
                    var onTuesday = reader["on_tuesday"];
                    var onWednesday = reader["on_wednesday"];
                    var onThursday = reader["on_thursday"];
                    var onFriday = reader["on_friday"];
                    var onSaturday = reader["on_saturday"];
                    var onSunday = reader["on_sunday"];

                    return new DbDays()
                    {
                        OnMonday = Convert.ToBoolean((long)onMonday),
                        OnTuesday = Convert.ToBoolean((long)onTuesday),
                        OnWednesday = Convert.ToBoolean((long)onWednesday),
                        OnThursday = Convert.ToBoolean((long)onThursday),
                        OnFriday = Convert.ToBoolean((long)onFriday),
                        OnSaturday = Convert.ToBoolean((long)onSaturday),
                        OnSunday = Convert.ToBoolean((long)onSunday),
                    };
                }
            }
            return null;

        }
    }
}
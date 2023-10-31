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

    /// <inheritdoc cref="IDaysTableRepository"/>.
    public class DaysTableRepository : IDaysTableRepository
    {
        /// <inheritdoc/>
        public async Task<bool> InsertIntoDaysTableAsync(PreparedDaysTableData daysTableData, string dbName, long habitId)
        {
            await using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var commandDaysTable = new SqliteCommand(DaysTableExpression.InsertDays, connection);
                var idDaysParam = new SqliteParameter("@habit_id", habitId);
                var mondayParam = new SqliteParameter("@monday", daysTableData.DaysAndReminds["monday"]);
                var tuesdayParam = new SqliteParameter("@tuesday", daysTableData.DaysAndReminds["tuesday"]);
                var wednesdayParam = new SqliteParameter("@wednesday", daysTableData.DaysAndReminds["wednesday"]);
                var thursdayParam = new SqliteParameter("@thursday", daysTableData.DaysAndReminds["thursday"]);
                var fridayParam = new SqliteParameter("@friday", daysTableData.DaysAndReminds["friday"]);
                var saturdayParam = new SqliteParameter("@saturday", daysTableData.DaysAndReminds["saturday"]);
                var sundayParam = new SqliteParameter("@sunday", daysTableData.DaysAndReminds["sunday"]);

                commandDaysTable.Parameters.Add(idDaysParam);
                commandDaysTable.Parameters.Add(mondayParam);
                commandDaysTable.Parameters.Add(tuesdayParam);
                commandDaysTable.Parameters.Add(wednesdayParam);
                commandDaysTable.Parameters.Add(thursdayParam);
                commandDaysTable.Parameters.Add(fridayParam);
                commandDaysTable.Parameters.Add(saturdayParam);
                commandDaysTable.Parameters.Add(sundayParam);

                if(commandDaysTable.ExecuteNonQuery() != null){
                    return true;
                }
                else return false; 

            }
        }

    }
}

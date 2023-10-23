using Microsoft.Data.Sqlite;
using LifeHabitTracker.DataAccessLayer.Entities;
using LifeHabitTracker.DataAccessLayer.Interfaces;


namespace LifeHabitTracker.DataAccessLayer.Impls
{

    /// <summary>
    /// Класс взаимодейтсвия с БД
    /// </summary>
    public class DataManage : IDataManage
    {

        /// <inheritdoc/>
        public bool WriteHabitInfo(string name, string desc, long chatId, int isGood, Dictionary<string, int> daysAndReminds, IReadOnlyCollection<string> times)
        {
  
            object result = null;

            using (var connection = new SqliteConnection("Data Source=Habits.db"))
            {
                connection.Open();

                #region "Запись в таблицу habit"
                SqliteCommand commandHabitTable = new SqliteCommand(Expression.InsertHabitTable, connection);
                SqliteParameter nameParam = new SqliteParameter("@name", name);
                SqliteParameter descParam = new SqliteParameter("@desc", desc);
                SqliteParameter chatIdParam = new SqliteParameter("@chatid", chatId);
                SqliteParameter isGoodParam = new SqliteParameter("@isgood", isGood);

                commandHabitTable.Parameters.Add(nameParam);
                commandHabitTable.Parameters.Add(descParam);
                commandHabitTable.Parameters.Add(chatIdParam);
                commandHabitTable.Parameters.Add(isGoodParam);

                int habitId = commandHabitTable.ExecuteNonQuery();
                //int habitId = (int)commandHabitTable.ExecuteScalar();

                #endregion

                if (daysAndReminds != null || times != null)
                {
                    #region "Запись в таблицу days"
                    SqliteCommand commandDaysTable = new SqliteCommand(Expression.InsertDaysTable, connection);
                    SqliteParameter idDaysParam = new SqliteParameter("@id", habitId);
                    SqliteParameter mondayParam = new SqliteParameter("@monday", daysAndReminds["monday"]);
                    SqliteParameter tuesdayParam = new SqliteParameter("@tuesday", daysAndReminds["tuesday"]);
                    SqliteParameter wednesdayParam = new SqliteParameter("@wednesday", daysAndReminds["wednesday"]);
                    SqliteParameter thursdayParam = new SqliteParameter("@thursday", daysAndReminds["thursday"]);
                    SqliteParameter fridayParam = new SqliteParameter("@friday", daysAndReminds["friday"]);
                    SqliteParameter saturdayParam = new SqliteParameter("@saturday", daysAndReminds["saturday"]);
                    SqliteParameter sundayParam = new SqliteParameter("@sunday", daysAndReminds["sunday"]);

                    commandDaysTable.Parameters.Add(idDaysParam);
                    commandDaysTable.Parameters.Add(mondayParam);
                    commandDaysTable.Parameters.Add(tuesdayParam);
                    commandDaysTable.Parameters.Add(wednesdayParam);
                    commandDaysTable.Parameters.Add(thursdayParam);
                    commandDaysTable.Parameters.Add(fridayParam);
                    commandDaysTable.Parameters.Add(saturdayParam);
                    commandDaysTable.Parameters.Add(sundayParam);

                    commandDaysTable.ExecuteNonQuery();
                    #endregion

                    #region "Запись в таблицу time"
                    foreach (var time in times)
                    {
                        SqliteCommand commandTimeTable = new SqliteCommand(Expression.InsertTimeTable, connection);
                        SqliteParameter idTimeParam = new SqliteParameter("@id", habitId);
                        SqliteParameter timeParam = new SqliteParameter("@time", time);

                        commandTimeTable.Parameters.Add(idTimeParam);
                        commandTimeTable.Parameters.Add(timeParam);

                        result = commandTimeTable.ExecuteNonQuery();

                    }
                    #endregion
                }
                else
                {
                    if (habitId != null) return true;
                    else return false;
                }

            }

            if (result != null) return true;
            else return false;

        }
    }
}

/*
using (var firstTransaction = firstConnection.BeginTransaction())
{
    var updateCommand = firstConnection.CreateCommand();
    updateCommand.CommandText =
    @"
        UPDATE data
        SET value = 'dirty'
    ";
    updateCommand.ExecuteNonQuery();

    // Without ReadUncommitted, the command will time out since the table is locked
    // while the transaction on the first connection is active
    using (secondConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
    {
        var queryCommand = secondConnection.CreateCommand();
        queryCommand.CommandText =
        @"
            SELECT *
            FROM data
        ";
        var value = (string)queryCommand.ExecuteScalar();
        Console.WriteLine($"Value: {value}");
    }

    firstTransaction.Rollback();

}



    [Windows.Foundation.Metadata.ContractVersion(typeof(Windows.Foundation.UniversalApiContract), 65536)]
    [Windows.Foundation.Metadata.MarshalingBehavior(Windows.Foundation.Metadata.MarshalingType.Agile)]
    [Windows.Foundation.Metadata.Threading(Windows.Foundation.Metadata.ThreadingModel.MTA)]
    public sealed class ApplicationData : IDisposable
    {
       public void Dispose() { }
    }






    public async static void InitializeDatabase()
    {
        await ApplicationData.Current.LocalFolder
                .CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
        string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path,
                                        "sqliteSample.db");
        using (var db = new SqliteConnection($"Filename={dbpath}"))
        {
            db.Open();

            string tableCommand = "CREATE TABLE IF NOT " +
                "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                "Text_Entry NVARCHAR(2048) NULL)";

            var createTable = new SqliteCommand(tableCommand, db);

            createTable.ExecuteReader();
        }
    }
    

















*/
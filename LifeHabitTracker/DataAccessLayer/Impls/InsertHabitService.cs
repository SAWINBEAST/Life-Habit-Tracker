using LifeHabitTracker.DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;

namespace LifeHabitTracker.DataAccessLayer.Impls
{
    ///<inheritdoc cref="IInsertHabitService"/>
    public class InsertHabitService :IInsertHabitService
    {
        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу days
        /// </summary>
        private readonly IHabitsTableRepository _habitsManager;

        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу habits
        /// </summary>
        private readonly IDaysTableRepository _daysManager;

        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу times
        /// </summary>
        private readonly ITimesTableRepository _timesManager;

        public InsertHabitService(IHabitsTableRepository habitsManager, IDaysTableRepository daysManager, ITimesTableRepository timesManager)
        {
            _habitsManager = habitsManager;
            _daysManager = daysManager;
            _timesManager = timesManager;
        }

        ///<inheritdoc/>
        public async Task<bool> InsertHabitAsync(PreparedHabitsTableData preparedHabits, PreparedDaysTableData preparedDays, PreparedTimesTableData preparedTimes, string dbName)
        {
            string connectionString = $"Data Source={dbName}";

            await using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                //TODO:Оптимизировать систему проверки и не использольвать временную переменную.
                var insertResult = new bool();
 
                var habitId = await _habitsManager.InsertIntoHabitsTableAsync(preparedHabits, connection, transaction);

                if (preparedHabits.IsGood)
                {
                    var resultOfDaysInsert = await _daysManager.InsertIntoDaysTableAsync(preparedDays, habitId, connection, transaction);
                    var resultOfTimesInsert = await _timesManager.InsertIntoTimesTableAsync(preparedTimes, habitId, connection, transaction);

                    if (resultOfDaysInsert == true && resultOfTimesInsert == true)
                    {
                        insertResult = true;
                    }
                    else insertResult = false;

                }
                else if (habitId != null) insertResult = true;
                else insertResult = false;

                transaction.Commit();
                return insertResult;
            }
        }
    }
}

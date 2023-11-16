using LifeHabitTracker.DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using LifeHabitTracker.Entities.PreparedData;
using LifeHabitTracker.Entities;

namespace LifeHabitTracker.DataAccessLayer.Impls
{
    ///<inheritdoc cref="IInsertHabitService"/>
    internal class InsertHabitService :IInsertHabitService
    {
        /// <summary>
        /// Информация о подключаемой БД
        /// </summary>
        public static DataBaseConnect _dBConfig;

        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу days
        /// </summary>
        private readonly IHabitsRepository _habitsRepository;

        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу habits
        /// </summary>
        private readonly IDaysRepository _daysRepository;

        /// <summary>
        /// Объект выполнения записи данных в БД, в таблицу times
        /// </summary>
        private readonly ITimesRepository _timesRepository;

        public InsertHabitService(IHabitsRepository habitsRepository, IDaysRepository daysRepository, ITimesRepository timesRepository, DataBaseConnect dBConfig)
        {
            _habitsRepository = habitsRepository;
            _daysRepository = daysRepository;
            _timesRepository = timesRepository;
            _dBConfig = dBConfig;
        }

        ///<inheritdoc/>
        public async Task<bool> InsertHabitAsync(DbHabits preparedHabits, DbDays preparedDays, DbTimes preparedTimes)
        {

            using var connection = new SqliteConnection(_dBConfig.DBName);
            connection.Open();
            var transaction = connection.BeginTransaction();

            //TODO:Оптимизировать систему проверки и не использольвать временную переменную.
            var insertResult = false;
 
            var habitId = await _habitsRepository.InsertIntoHabitsTableAsync(preparedHabits, connection, transaction);

            if (habitId != null)
            {
                if (preparedHabits.IsGood)
                {
                    var resultOfDaysInsert = _daysRepository.InsertIntoDaysTableAsync(preparedDays, habitId, connection, transaction);
                    var resultOfTimesInsert = _timesRepository.InsertIntoTimesTableAsync(preparedTimes, habitId, connection, transaction);

                    insertResult = await resultOfDaysInsert && await resultOfTimesInsert;
                }
                else 
                    insertResult = true;
            }

            transaction.Commit();
            return insertResult;
            
        }
    }
}

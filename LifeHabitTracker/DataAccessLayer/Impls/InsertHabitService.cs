using LifeHabitTracker.DataAccessLayer.Entities.PreparedData;
using LifeHabitTracker.DataAccessLayer.Interfaces;
using LifeHabitTracker.DataAccessLayer.Interfaces.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

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

        public InsertHabitService(IHabitsRepository habitsRepository, IDaysRepository daysRepository, ITimesRepository timesRepository, IOptions<DataBaseConnect> options)
        {
            _habitsRepository = habitsRepository;
            _daysRepository = daysRepository;
            _timesRepository = timesRepository;
            _dBConfig = options.Value;
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

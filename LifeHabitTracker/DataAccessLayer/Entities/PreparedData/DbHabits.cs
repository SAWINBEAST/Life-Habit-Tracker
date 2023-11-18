﻿namespace LifeHabitTracker.DataAccessLayer.Entities.PreparedData
{
    /// <summary>
    /// Класс подготовленной информации для таблицы habits
    /// </summary>
    public class DbHabits
    {
        /// <summary>
        /// Идентификатор привычки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название привычки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание привычки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор чата клиента, пользующего привычкой
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Тип привычки
        /// </summary>
        public bool IsGood { get; set; }
    }
}
using System;


namespace crm.contracts
{
    /// <summary>
    /// СНТ (информация)
    /// </summary>
    public class GardenSocietyInfo
    {
        public string Name { get; set; }

        public bool Enabled { get; set; }
    }

    /// <summary>
    /// СНТ
    /// </summary>
    public class GardenSociety : GardenSocietyInfo
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        public long Id { get; set; }
    }

    /// <summary>
    /// СНТ (истоиия)
    /// </summary>
    public class GardenSocietyHistory : GardenSocietyInfo
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор объекта истории
        /// </summary>
        public long HistoryId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, отвественного за историю
        /// </summary>
        public long ChangedUserId { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime Changed { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool? Deleted { get; set; }

        /// <summary>
        /// Сырые данные истриии
        /// </summary>
        public string RowData { get; set; }
    }
}

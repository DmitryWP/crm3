using System;
using System.Runtime.Serialization;

namespace crm.contracts
{
    /// <summary>
    /// СНТ (информация)
    /// </summary>
    [DataContract]
    public class GardenSocietyInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool Enabled { get; set; }
    }

    /// <summary>
    /// СНТ
    /// </summary>
    [DataContract]
    public class GardenSociety : GardenSocietyInfo
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [DataMember]
        public long Id { get; set; }
    }

    /// <summary>
    /// СНТ (истоиия)
    /// </summary>
    [DataContract]
    public class GardenSocietyHistory : GardenSocietyInfo
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор объекта истории
        /// </summary>
        [DataMember]
        public long HistoryId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, отвественного за историю
        /// </summary>
        [DataMember]
        public long ChangedUserId { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        [DataMember]
        public DateTime Changed { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        [DataMember]
        public bool? Deleted { get; set; }

        /// <summary>
        /// Сырые данные истриии
        /// </summary>
        [DataMember]
        public string RowData { get; set; }
    }
}

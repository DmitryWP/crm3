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
        public string name { get; set; }

        [DataMember]
        public bool enabled { get; set; }
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
        public long id { get; set; }
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
        public long id { get; set; }

        /// <summary>
        /// Идентификатор объекта истории
        /// </summary>
        [DataMember]
        public long historyId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, отвественного за историю
        /// </summary>
        [DataMember]
        public long changedUserId { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        [DataMember]
        public DateTime changed { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        [DataMember]
        public bool? deleted { get; set; }

        /// <summary>
        /// Сырые данные истриии
        /// </summary>
        [DataMember]
        public string rowData { get; set; }
    }
}

using System;
using System.Runtime.Serialization;

namespace crm.contracts
{
    /// <summary>
    /// Контакт (информация)
    /// </summary>
    [DataContract]
    public class ContactInfo
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        [DataMember]
        public string Family { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [DataMember]
        public string FName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [DataMember]
        public string SName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// Контактный телефон 1
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// Контактный телефон 2
        /// </summary>
        [DataMember]
        public string Phone2 { get; set; }

        /// <summary>
        /// Контактный телефон 3
        /// </summary>
        [DataMember]
        public string Phone3 { get; set; }

        [DataMember]
        public string EMail { get; set; }

    }

    /// <summary>
    /// Контакт 
    /// </summary>
    public class Contact : ContactInfo
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [DataMember]
        public long Id { get; set; }
    }

    /// <summary>
    /// Контакт (история)
    /// </summary>
    public class ContactHistory : ContactInfo
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

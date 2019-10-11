using System;

namespace crm.contracts
{
    /// <summary>
    /// Контакт (информация)
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string SName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Контактный телефон 1
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Контактный телефон 2
        /// </summary>
        public string Phone2 { get; set; }

        /// <summary>
        /// Контактный телефон 3
        /// </summary>
        public string Phone3 { get; set; }

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

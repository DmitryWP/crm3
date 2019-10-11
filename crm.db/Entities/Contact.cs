using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace crm.db.entities
{
    /// <summary>
    /// Контакт (информация)
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        [StringLength(50)]
        public string Family { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [StringLength(50)]
        public string FName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [StringLength(50)]
        public string SName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [StringLength(50)]
        public string Address { get; set; }

        /// <summary>
        /// Контактный телефон 1
        /// </summary>
        [StringLength(11)]
        public string Phone { get; set; }

        /// <summary>
        /// Контактный телефон 2
        /// </summary>
        [StringLength(11)]
        public string Phone2 { get; set; }

        /// <summary>
        /// Контактный телефон 3
        /// </summary>
        [StringLength(11)]
        public string Phone3 { get; set; }

        [StringLength(50)]
        public string EMail { get; set; }

    }

    /// <summary>
    /// Контакт 
    /// </summary>
    [Table("Contact", Schema = "crm")]
    public class Contact : ContactInfo, IEntity
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }

    /// <summary>
    /// Контакт (история)
    /// </summary>
    [Table("Contact_H", Schema = "crm")]
    public class ContactHistory : ContactInfo, IEntity, IHistory
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор объекта истории
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

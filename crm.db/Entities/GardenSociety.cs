using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace crm.db.entities
{
    /// <summary>
    /// СНТ (информация)
    /// </summary>
    public class GardenSocietyInfo
    {
        [StringLength(50)]
        public string Name { get; set; }

        public bool Enabled { get; set; }
    }

    /// <summary>
    /// СНТ
    /// </summary>
    [Table("GardenSociety", Schema = "crm")]
    public class GardenSociety: GardenSocietyInfo, IEntity
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }

    /// <summary>
    /// СНТ (истоиия)
    /// </summary>
    [Table("GardenSociety_H", Schema = "crm")]
    public class GardenSocietyHistory : GardenSocietyInfo, IEntity, IHistory
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

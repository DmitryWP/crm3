using crm.classess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace crm.db.entities
{
    /// <summary>
    /// Базовые цены
    /// </summary>
    [Table("Settings", Schema = "crm")]
    public class Settings : IEntity
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Тип настройки
        /// </summary>
        public SettingType Kind { get; set; }

        /// <summary>
        /// Дата начала действия настройки
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата окончания действия настройки
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Значение (цена)
        /// </summary>
        public decimal Price { get; set; }
    }
}

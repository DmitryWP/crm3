using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace crm.db.entities
{
    /// <summary>
    /// Улицы (информация)
    /// </summary>
    [Table("Street", Schema = "crm")]
    public class Street: IEntity
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор СНТ
        /// </summary>
        public long GardenSocietyId { get; set; }

        /// <summary>
        /// СНТ
        /// </summary>
        [ForeignKey("GardenSocietyId")]
        public GardenSociety GardenSociety { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

    }
}

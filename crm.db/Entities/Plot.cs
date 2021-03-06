﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace crm.db.entities
{

    /// <summary>
    /// Садовый участок (информация)
    /// </summary>
    public class PlotInfo
    {
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
        /// Лицевой счет
        /// </summary>
        [StringLength(50)]
        public string PersonalAccount { get; set; }

        /// <summary>
        /// Площадь участка
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// Идентификатор улицы СНТ
        /// </summary>
        public long StreetId { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [ForeignKey("StreetId")]
        public Street Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        public int HouseNumber { get; set; }

        /// <summary>
        /// Идентификатор владельца участка
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        /// Владелец участка
        /// </summary>
        [ForeignKey("OwnerId")]
        public Contact Owner { get; set; }

        public int Age { get; set; }
    }

    /// <summary>
    /// Садовый участок 
    /// </summary>
    [Table("Plot", Schema = "crm")]
    public class Plot : PlotInfo, IEntity
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }

    /// <summary>
    /// Садовый участок (история)
    /// </summary>
    [Table("Plot_H", Schema = "crm")]
    public class PlotHistory : PlotInfo, IEntity, IHistory
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

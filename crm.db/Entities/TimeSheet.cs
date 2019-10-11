using crm.classess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace crm.db.entities
{
    /// <summary>
    /// Табель оплат (информация)
    /// </summary>
    public class TimeSheetInfo
    {
        /// <summary>
        /// Дата внесения оплаты
        /// </summary>
        public DateTime TSDate { get; set; }

        /// <summary>
        /// Идентификатор садового участка
        /// </summary>
        public long PlotId { get; set; }

        /// <summary>
        /// Садовый учсток
        /// </summary>
        [ForeignKey("PlotId")]
        public Plot Plot { get; set; }

        /// <summary>
        /// Цель оплаты
        /// </summary>
        public SettingType Target { get; set; }

        /// <summary>
        /// Показание счетчика
        /// </summary>
        public long CounterReading { get; set; }

        /// <summary>
        /// Идентификатор оплаты за...
        /// </summary>
        public long SettingId { get; set; }

        /// <summary>
        /// Оплата за...
        /// </summary>
        [ForeignKey("SettingId")]
        public Settings Settings { get; set; }

        /// <summary>
        /// Направление оплаты
        /// </summary>
        public TimeSheetDirect Direct { get; set; }

        /// <summary>
        /// Объем услуги
        /// </summary>
        public long Volume { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Price { get; set; }
    }

    [Table("TimeSheet", Schema = "crm")]
    public class TimeSheet : TimeSheetInfo, IEntity
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }

    [Table("TimeSheet_H", Schema = "crm")]
    public class TimeSheetHistory : TimeSheetInfo, IEntity, IHistory
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

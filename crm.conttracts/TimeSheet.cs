using crm.classess;
using System;
using System.Runtime.Serialization;

namespace crm.contracts
{
    /// <summary>
    /// Табель оплат (информация)
    /// </summary>
    [DataContract]
    public class TimeSheetInfo
    {
        /// <summary>
        /// Дата внесения оплаты
        /// </summary>
        [DataMember]
        public DateTime TSDate { get; set; }

        /// <summary>
        /// Идентификатор садового участка
        /// </summary>
        [DataMember]
        public long PlotId { get; set; }

        /// <summary>
        /// Садовый учсток
        /// </summary>
        [DataMember]
        public Plot Plot { get; set; }

        /// <summary>
        /// Цель оплаты
        /// </summary>
        [DataMember]
        public SettingType Target { get; set; }

        /// <summary>
        /// Показание счетчика
        /// </summary>
        [DataMember]
        public long CounterReading { get; set; }

        /// <summary>
        /// Идентификатор оплаты за...
        /// </summary>
        [DataMember]
        public long SettingId { get; set; }

        /// <summary>
        /// Оплата за...
        /// </summary>
        [DataMember]
        public Settings Settings { get; set; }

        /// <summary>
        /// Направление оплаты
        /// </summary>
        [DataMember]
        public TimeSheetDirect Direct { get; set; }

        /// <summary>
        /// Объем услуги
        /// </summary>
        [DataMember]
        public long Volume { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }
    }

    [DataContract]
    public class TimeSheet : TimeSheetInfo
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class TimeSheetHistory : TimeSheetInfo
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

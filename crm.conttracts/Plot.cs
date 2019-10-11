using System;

namespace crm.contracts
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
        public GardenSociety GardenSociety { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
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
        public Contact Owner { get; set; }

        public int Age { get; set; }
    }

    /// <summary>
    /// Садовый участок 
    /// </summary>
    public class Plot : PlotInfo
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        public long Id { get; set; }
    }

    /// <summary>
    /// Садовый участок (история)
    /// </summary>
    public class PlotHistory : PlotInfo
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

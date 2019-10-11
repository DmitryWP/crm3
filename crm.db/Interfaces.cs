using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace crm.db
{
    /// <summary>
    /// Сущность
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        long Id { get; set; }
    }

    /// <summary>
    /// История сущности
    /// </summary>
    public interface IHistory
    {
        /// <summary>
        /// Идентификатор объекта истории сущности
        /// </summary>
        long HistoryId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, отвественного за историю
        /// </summary>
        long ChangedUserId { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        DateTime Changed { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        bool? Deleted { get; set; }
        
        /// <summary>
        /// Сырые данные истриии
        /// </summary>
        string RowData { get; set; }
    }

}

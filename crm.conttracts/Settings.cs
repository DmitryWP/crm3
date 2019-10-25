using crm.classess;
using System;
using System.Runtime.Serialization;

namespace crm.contracts
{
    /// <summary>
    /// Базовые цены
    /// </summary>
    [DataContract]
    public class Settings 
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Тип настройки
        /// </summary>
        [DataMember]
        public SettingType Kind { get; set; }

        /// <summary>
        /// Дата начала действия настройки
        /// </summary>
        [DataMember]
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата окончания действия настройки
        /// </summary>
        [DataMember]
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Значение (цена)
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }
    }
}

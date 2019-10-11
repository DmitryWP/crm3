using System;

namespace crm.contracts
{
    /// <summary>
    /// Тип элемента настройки
    /// </summary>
    public enum SettingType : long
    {
        /// <summary>
        /// Начальные значения
        /// </summary>
        Initial = 0,

        /// <summary>
        /// Целевые взносы
        /// </summary>
        InstallmentTarget = 1,

        /// <summary>
        /// Членские взносы
        /// </summary>
        MembershipFee = 2,

        /// <summary>
        /// Цена за электрожнергию (кВт/ч)
        /// </summary>
        PriceOfElectricity = 4,

        /// <summary>
        /// Цена за водоснабжение (куб.м)
        /// </summary>
        WaterPrice = 8,

        /// <summary>
        /// Обслуживание водопровода
        /// </summary>
        PlumbingService = 16

    }

    /// <summary>
    /// Базовые цены
    /// </summary>
    public class Settings 
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
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

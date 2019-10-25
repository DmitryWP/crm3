using System;
using System.Runtime.Serialization;

namespace crm.classess
{
    /// <summary>
    /// Тип элемента настройки
    /// </summary>
    [DataContract]
    [Flags]
    public enum SettingType : long
    {
        /// <summary>
        /// Начальные значения
        /// </summary>
        [EnumMember]
        Initial = 0,

        /// <summary>
        /// Целевые взносы
        /// </summary>
        [EnumMember]
        InstallmentTarget = 1,

        /// <summary>
        /// Членские взносы
        /// </summary>
        [EnumMember]
        MembershipFee = 2,

        /// <summary>
        /// Цена за электрожнергию (кВт/ч)
        /// </summary>
        [EnumMember]
        PriceOfElectricity = 4,

        /// <summary>
        /// Цена за водоснабжение (куб.м)
        /// </summary>
        [EnumMember]
        WaterPrice = 8,

        /// <summary>
        /// Обслуживание водопровода
        /// </summary>
        [EnumMember]
        PlumbingService = 16

    }


    /// <summary>
    /// Тип оплаты
    /// </summary>
    [DataContract]
    public enum TimeSheetDirect
    {
        /// <summary>
        /// Задолженность
        /// </summary>
        [EnumMember]
        Debt,

        /// <summary>
        /// текущая оплата
        /// </summary>
        [EnumMember]
        Current,

        /// <summary>
        /// Оплата вперед
        /// </summary>
        [EnumMember]
        Advance
    }
}

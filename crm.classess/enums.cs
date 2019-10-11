using System;

namespace crm.classess
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
    /// Тип оплаты
    /// </summary>
    public enum TimeSheetDirect
    {
        /// <summary>
        /// Задолженность
        /// </summary>
        Debt,

        /// <summary>
        /// текущая оплата
        /// </summary>
        Current,

        /// <summary>
        /// Оплата вперед
        /// </summary>
        Advance
    }

}

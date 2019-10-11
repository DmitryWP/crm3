using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elite.ef6
{
    /// <summary>
    /// Атрибут установки поддержки Unicide для строковых полей
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class IsUnicode : Attribute
    {
        /// <summary>
        /// указывает, поддерживает ли строка символы unicode
        /// </summary>
        public bool Unicode { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="isUnicode">указывает, поддерживает ли строка символы unicode</param>
        public IsUnicode(bool isUnicode)
        {
            Unicode = isUnicode;
        }
    }

    /// <summary>
    /// Атрибут установки точности и масштаба типа decimal
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class HasPrecision : Attribute
    {
        /// <summary>
        /// Точность
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// Масштаб
        /// </summary>
        public byte Scale { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="precision">Точность</param>
        /// <param name="scale">Масштаб</param>
        public HasPrecision(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }
    }
}


using System.Runtime.Serialization;

namespace crm.contracts
{
    /// <summary>
    /// Улицы (информация)
    /// </summary>
    [DataContract]
    public class Street
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор СНТ
        /// </summary>
        [DataMember]
        public long GardenSocietyId { get; set; }

        /// <summary>
        /// СНТ
        /// </summary>
        [DataMember]
        public GardenSociety GardenSociety { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [DataMember]
        public string Name { get; set; }

    }
}

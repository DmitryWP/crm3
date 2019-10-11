
namespace crm.contracts
{
    /// <summary>
    /// Улицы (информация)
    /// </summary>
    public class Street
    {
        /// <summary>
        /// Иденитификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор СНТ
        /// </summary>
        public long GardenSocietyId { get; set; }

        /// <summary>
        /// СНТ
        /// </summary>
        public GardenSociety GardenSociety { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Name { get; set; }

    }
}

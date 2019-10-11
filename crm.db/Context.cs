using crm.db.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace crm.db
{
    public class Context : DbContext
    {
        /// <summary>
        /// Настройки
        /// </summary>
        public DbSet<Settings> Settings { get; set; }

        /// <summary>
        /// Контакты
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Истории контактов
        /// </summary>
        public DbSet<ContactHistory> ContactHistories { get; set; }

        /// <summary>
        /// СНТ
        /// </summary>
        public DbSet<GardenSociety> GardenSocieties { get; set; }

        /// <summary>
        /// Истории СНТ
        /// </summary>
        public DbSet<GardenSocietyHistory> GardenSocietyHistories { get; set; }

        /// <summary>
        /// Садовые участки
        /// </summary>
        public DbSet<Plot> Plots { get; set; }

        /// <summary>
        /// Истории садовых участков
        /// </summary>
        public DbSet<PlotHistory> PlotHistories { get; set; }

        /// <summary>
        /// Оплаты
        /// </summary>
        public DbSet<TimeSheet> TimeSheets { get; set; }

        /// <summary>
        /// Истоиии оплат
        /// </summary>
        public DbSet<TimeSheetHistory> TimeSheetHistories { get; set; }


        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MORDER;Database=crmDb;Trusted_Connection=True;");
            // data source = MORDER; initial catalog = elite; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes()
                                                                    .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}

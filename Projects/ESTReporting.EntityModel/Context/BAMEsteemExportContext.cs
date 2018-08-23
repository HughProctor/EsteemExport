using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ESTReporting.EntityModel.Models;

namespace ESTReporting.EntityModel.Context
{
    public class BAMEsteemExportContext : DbContext
    {
        public BAMEsteemExportContext() : base(ConfigurationManager.ConnectionStrings["BAMEsteemExportDb"].ConnectionString)
        {
        }

        public BAMEsteemExportContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<ESTPart> ESTParts { get; set; }
        public DbSet<PartManufacturer> PartManufacturers { get; set; }
        public DbSet<ESTPartDescription> ESTPartDescriptions { get; set; }
        public DbSet<PartModel> PartModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Item>().ToTable("Items");
        }
    }
}

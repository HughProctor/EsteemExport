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
        public DbSet<BAM_Reporting> BAM_Reporting { get; set; }
        public DbSet<EST_SCAudit> SCAudit { get; set; }
        public DbSet<EST_SCAuditDeploy> SCAuditDeploy { get; set; }
        public DbSet<BAM_HardwareTemplate_Full> BAM_HardwareTemplate_Full { get; set; }
        public DbSet<BAM_TargetHardwareAssetHasCostCenter> BAM_TargetHardwareAssetHasCostCenter { get; set; }
        public DbSet<BAM_TargetHardwareAssetHasLocation> BAM_TargetHardwareAssetHasLocation { get; set; }
        public DbSet<BAM_TargetHardwareAssetHasPrimaryUser> BAM_TargetHardwareAssetHasPrimaryUser { get; set; }
        public DbSet<BAM_AssetStatus> BAM_AssetStatus { get; set; }
        public DbSet<BAM_HardwareAssetStatus> BAM_HardwareAssetStatus { get; set; }
        public DbSet<BAM_HardwareAssetType> BAM_HardwareAssetType { get; set; }
        public DbSet<BAM_ManufacturerEnum> BAM_ManufacturerEnum { get; set; }
        public DbSet<BAM_ModelEnum> BAM_ModelEnum { get; set; }
        public DbSet<BAM_Deployments> BAM_Deployments { get; set; }
        public DbSet<ServiceProgressReport> ServiceProgressReport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));
        }
    }
}

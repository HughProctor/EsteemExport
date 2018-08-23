namespace ESTReporting.EntityModel.Migrations
{
    using ESTReporting.EntityModel.SeedData;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.BAMEsteemExportContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
        }

        protected override void Seed(Context.BAMEsteemExportContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var count = 1;
            PartManufacturerData.GetManufacturers().ForEach(item =>
            {
                item.Id = count;
                item.CreatedDate = DateTime.Now;
                context.PartManufacturers.AddOrUpdate(x => x.Id, item);
                count++;
            });
            count = 1;
            PartModelData.GetModels().ForEach(item =>
            {
                item.Id = count;
                item.CreatedDate = DateTime.Now;
                context.PartModels.AddOrUpdate(x => x.Id, item);
                count++;
            });
        }
    }
}

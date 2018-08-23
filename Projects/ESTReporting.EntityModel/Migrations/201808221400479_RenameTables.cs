namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ESTPartDescriptions", newName: "ESTPartDescription");
            RenameTable(name: "dbo.ESTParts", newName: "ESTPart");
            RenameTable(name: "dbo.PartManufacturers", newName: "PartManufacturer");
            RenameTable(name: "dbo.PartModels", newName: "PartModel");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PartModel", newName: "PartModels");
            RenameTable(name: "dbo.PartManufacturer", newName: "PartManufacturers");
            RenameTable(name: "dbo.ESTPart", newName: "ESTParts");
            RenameTable(name: "dbo.ESTPartDescription", newName: "ESTPartDescriptions");
        }
    }
}

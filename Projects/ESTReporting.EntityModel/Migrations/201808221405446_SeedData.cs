namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ESTPartDescription", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.ESTPart", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.PartManufacturer", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.PartModel", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PartModel", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PartManufacturer", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ESTPart", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ESTPartDescription", "UpdatedDate", c => c.DateTime(nullable: false));
        }
    }
}

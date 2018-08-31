namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_ServiceProgressReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceProgressReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        EsteemExtractDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        BAMExportDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProcessSuccessFlag = c.Boolean(nullable: false),
                        NewItemCount = c.Int(),
                        LocationChangeCount = c.Int(),
                        AssetTagChangeCount = c.Int(),
                        DeployedCount = c.Int(),
                        ReturnedCount = c.Int(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServiceProgressReport");
        }
    }
}

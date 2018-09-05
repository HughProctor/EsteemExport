namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EST_BAM_ModelLookup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EST_BAM_ModelLookup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EST_ModelDescription = c.String(),
                        BAM_ModelDescription = c.String(),
                        IsActive = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EST_BAM_ModelLookup");
        }
    }
}

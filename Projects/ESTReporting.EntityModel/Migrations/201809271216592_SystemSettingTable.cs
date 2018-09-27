namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SystemSettingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        SortOrder = c.Int(nullable: false),
                        Parameter01 = c.String(),
                        Parameter02 = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ServiceProgressReport", "RetiredCount", c => c.Int());
            AddColumn("dbo.ServiceProgressReport", "DisposedCount", c => c.Int());
            AddColumn("dbo.ServiceProgressReport", "SwappedCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceProgressReport", "SwappedCount");
            DropColumn("dbo.ServiceProgressReport", "DisposedCount");
            DropColumn("dbo.ServiceProgressReport", "RetiredCount");
            DropTable("dbo.SystemSetting");
        }
    }
}

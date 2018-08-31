namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BAM_Deployments_AssetStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BAM_Deployments", "AssetStatus", c => c.Int(nullable: false));
            AddColumn("dbo.BAM_Reporting", "AssetStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BAM_Reporting", "AssetStatus");
            DropColumn("dbo.BAM_Deployments", "AssetStatus");
        }
    }
}

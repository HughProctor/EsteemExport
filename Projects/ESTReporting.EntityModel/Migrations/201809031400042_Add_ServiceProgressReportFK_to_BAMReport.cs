namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ServiceProgressReportFK_to_BAMReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BAM_Reporting", "ServiceProgressReport_Id", c => c.Int());
            CreateIndex("dbo.BAM_Reporting", "ServiceProgressReport_Id");
            AddForeignKey("dbo.BAM_Reporting", "ServiceProgressReport_Id", "dbo.ServiceProgressReport", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BAM_Reporting", "ServiceProgressReport_Id", "dbo.ServiceProgressReport");
            DropIndex("dbo.BAM_Reporting", new[] { "ServiceProgressReport_Id" });
            DropColumn("dbo.BAM_Reporting", "ServiceProgressReport_Id");
        }
    }
}

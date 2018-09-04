namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FK_ServiceProgressReport_Id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BAM_Reporting", "ServiceProgressReport_Id", "dbo.ServiceProgressReport");
            DropIndex("dbo.BAM_Reporting", new[] { "ServiceProgressReport_Id" });
            RenameColumn(table: "dbo.BAM_Reporting", name: "ServiceProgressReport_Id", newName: "ServiceProgressReportId");
            AlterColumn("dbo.BAM_Reporting", "ServiceProgressReportId", c => c.Int(nullable: false));
            CreateIndex("dbo.BAM_Reporting", "ServiceProgressReportId");
            AddForeignKey("dbo.BAM_Reporting", "ServiceProgressReportId", "dbo.ServiceProgressReport", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BAM_Reporting", "ServiceProgressReportId", "dbo.ServiceProgressReport");
            DropIndex("dbo.BAM_Reporting", new[] { "ServiceProgressReportId" });
            AlterColumn("dbo.BAM_Reporting", "ServiceProgressReportId", c => c.Int());
            RenameColumn(table: "dbo.BAM_Reporting", name: "ServiceProgressReportId", newName: "ServiceProgressReport_Id");
            CreateIndex("dbo.BAM_Reporting", "ServiceProgressReport_Id");
            AddForeignKey("dbo.BAM_Reporting", "ServiceProgressReport_Id", "dbo.ServiceProgressReport", "Id");
        }
    }
}

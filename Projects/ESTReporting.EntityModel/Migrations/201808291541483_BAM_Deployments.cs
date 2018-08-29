namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BAM_Deployments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BAM_Deployments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        BAM_HardwareTemplate_Exception_Id = c.Int(),
                        SCAudit_Item_Id = c.Int(),
                        SCAuditDeploy_Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BAM_HardwareTemplate_Full", t => t.BAM_HardwareTemplate_Exception_Id)
                .ForeignKey("dbo.EST_SCAudit", t => t.SCAudit_Item_Id)
                .ForeignKey("dbo.EST_SCAuditDeploy", t => t.SCAuditDeploy_Item_Id)
                .Index(t => t.BAM_HardwareTemplate_Exception_Id)
                .Index(t => t.SCAudit_Item_Id)
                .Index(t => t.SCAuditDeploy_Item_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BAM_Deployments", "SCAuditDeploy_Item_Id", "dbo.EST_SCAuditDeploy");
            DropForeignKey("dbo.BAM_Deployments", "SCAudit_Item_Id", "dbo.EST_SCAudit");
            DropForeignKey("dbo.BAM_Deployments", "BAM_HardwareTemplate_Exception_Id", "dbo.BAM_HardwareTemplate_Full");
            DropIndex("dbo.BAM_Deployments", new[] { "SCAuditDeploy_Item_Id" });
            DropIndex("dbo.BAM_Deployments", new[] { "SCAudit_Item_Id" });
            DropIndex("dbo.BAM_Deployments", new[] { "BAM_HardwareTemplate_Exception_Id" });
            DropTable("dbo.BAM_Deployments");
        }
    }
}

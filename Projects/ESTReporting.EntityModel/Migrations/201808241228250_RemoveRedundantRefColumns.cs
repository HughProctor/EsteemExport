namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRedundantRefColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BAM_Reporting", "SCAudit_Ref");
            DropColumn("dbo.BAM_Reporting", "SCAuditDeploy_Ref");
            DropColumn("dbo.BAM_Reporting", "BAM_HardwareTemplate_Exception_Ref");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BAM_Reporting", "BAM_HardwareTemplate_Exception_Ref", c => c.Int(nullable: false));
            AddColumn("dbo.BAM_Reporting", "SCAuditDeploy_Ref", c => c.Int(nullable: false));
            AddColumn("dbo.BAM_Reporting", "SCAudit_Ref", c => c.Int(nullable: false));
        }
    }
}

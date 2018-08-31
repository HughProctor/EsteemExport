namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ESTAudit_BaseClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EST_SCAudit", "AssetName", c => c.String());
            AddColumn("dbo.EST_SCAudit", "DisplayName", c => c.String());
            AddColumn("dbo.EST_SCAudit", "Manufacturer", c => c.String());
            AddColumn("dbo.EST_SCAudit", "Model", c => c.String());
            AddColumn("dbo.EST_SCAudit", "RequestUser", c => c.String());
            AddColumn("dbo.EST_SCAudit", "SerialNumber", c => c.String());
            AddColumn("dbo.EST_SCAuditDeploy", "AssetName", c => c.String());
            AddColumn("dbo.EST_SCAuditDeploy", "DisplayName", c => c.String());
            AddColumn("dbo.EST_SCAuditDeploy", "Manufacturer", c => c.String());
            AddColumn("dbo.EST_SCAuditDeploy", "Model", c => c.String());
            AddColumn("dbo.EST_SCAuditDeploy", "RequestUser", c => c.String());
            AddColumn("dbo.EST_SCAuditDeploy", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EST_SCAuditDeploy", "SerialNumber");
            DropColumn("dbo.EST_SCAuditDeploy", "RequestUser");
            DropColumn("dbo.EST_SCAuditDeploy", "Model");
            DropColumn("dbo.EST_SCAuditDeploy", "Manufacturer");
            DropColumn("dbo.EST_SCAuditDeploy", "DisplayName");
            DropColumn("dbo.EST_SCAuditDeploy", "AssetName");
            DropColumn("dbo.EST_SCAudit", "SerialNumber");
            DropColumn("dbo.EST_SCAudit", "RequestUser");
            DropColumn("dbo.EST_SCAudit", "Model");
            DropColumn("dbo.EST_SCAudit", "Manufacturer");
            DropColumn("dbo.EST_SCAudit", "DisplayName");
            DropColumn("dbo.EST_SCAudit", "AssetName");
        }
    }
}

namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_CallNum_CallRef : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EST_SCAuditDeploy", "Audit_Call_Num", c => c.String());
            AddColumn("dbo.EST_SCAuditDeploy", "Audit_Call_Ref", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EST_SCAuditDeploy", "Audit_Call_Ref");
            DropColumn("dbo.EST_SCAuditDeploy", "Audit_Call_Num");
        }
    }
}

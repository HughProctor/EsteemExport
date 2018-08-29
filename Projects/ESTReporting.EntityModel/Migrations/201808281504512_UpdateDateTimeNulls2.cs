namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTimeNulls2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EST_SCAudit", "Audit_Last_Update", c => c.DateTime());
            AlterColumn("dbo.EST_SCAudit", "Audit_Move_Date", c => c.DateTime());
            AlterColumn("dbo.EST_SCAuditDeploy", "Audit_Last_Update", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EST_SCAuditDeploy", "Audit_Last_Update", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EST_SCAudit", "Audit_Move_Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EST_SCAudit", "Audit_Last_Update", c => c.DateTime(nullable: false));
        }
    }
}

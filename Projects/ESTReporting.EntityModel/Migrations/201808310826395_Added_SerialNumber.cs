namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_SerialNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BAM_Deployments", "SerialNumber", c => c.String());
            AddColumn("dbo.BAM_Reporting", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BAM_Reporting", "SerialNumber");
            DropColumn("dbo.BAM_Deployments", "SerialNumber");
        }
    }
}

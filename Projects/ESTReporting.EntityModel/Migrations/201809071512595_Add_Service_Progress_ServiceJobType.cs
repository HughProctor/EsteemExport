namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Service_Progress_ServiceJobType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceProgressReport", "ServiceJobType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceProgressReport", "ServiceJobType");
        }
    }
}

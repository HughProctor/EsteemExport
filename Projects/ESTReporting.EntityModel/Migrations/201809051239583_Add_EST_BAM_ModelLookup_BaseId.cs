namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EST_BAM_ModelLookup_BaseId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EST_BAM_ModelLookup", "BaseId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EST_BAM_ModelLookup", "BaseId");
        }
    }
}

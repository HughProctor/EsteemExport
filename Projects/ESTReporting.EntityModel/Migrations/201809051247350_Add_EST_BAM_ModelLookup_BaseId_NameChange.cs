namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EST_BAM_ModelLookup_BaseId_NameChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EST_BAM_ModelLookup", "BAM_BaseId", c => c.String());
            DropColumn("dbo.EST_BAM_ModelLookup", "BaseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EST_BAM_ModelLookup", "BaseId", c => c.String());
            DropColumn("dbo.EST_BAM_ModelLookup", "BAM_BaseId");
        }
    }
}

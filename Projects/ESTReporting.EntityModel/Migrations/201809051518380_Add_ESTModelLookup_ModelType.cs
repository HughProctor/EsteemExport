namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ESTModelLookup_ModelType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EST_BAM_ModelLookup", "BAM_ModelType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EST_BAM_ModelLookup", "BAM_ModelType");
        }
    }
}

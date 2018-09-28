namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_EST_BAM_ModelLookup__EsteemAltCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EST_BAM_ModelLookup", "Esteem_Alt_ModelString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EST_BAM_ModelLookup", "Esteem_Alt_ModelString");
        }
    }
}

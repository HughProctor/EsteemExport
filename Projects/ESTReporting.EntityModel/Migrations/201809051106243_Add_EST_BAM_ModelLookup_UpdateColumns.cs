namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EST_BAM_ModelLookup_UpdateColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EST_BAM_ModelLookup", "EST_ManufacturerCode", c => c.String());
            AddColumn("dbo.EST_BAM_ModelLookup", "BAM_Name", c => c.String());
            AddColumn("dbo.EST_BAM_ModelLookup", "BAM_ModelString", c => c.String());
            AddColumn("dbo.EST_BAM_ModelLookup", "BAM_ManufacturerString", c => c.String());
            AddColumn("dbo.EST_BAM_ModelLookup", "BAM_DisplayName", c => c.String());
            DropColumn("dbo.EST_BAM_ModelLookup", "BAM_ModelDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EST_BAM_ModelLookup", "BAM_ModelDescription", c => c.String());
            DropColumn("dbo.EST_BAM_ModelLookup", "BAM_DisplayName");
            DropColumn("dbo.EST_BAM_ModelLookup", "BAM_ManufacturerString");
            DropColumn("dbo.EST_BAM_ModelLookup", "BAM_ModelString");
            DropColumn("dbo.EST_BAM_ModelLookup", "BAM_Name");
            DropColumn("dbo.EST_BAM_ModelLookup", "EST_ManufacturerCode");
        }
    }
}

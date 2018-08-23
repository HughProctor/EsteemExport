namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePartModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartModel", "EsteemCode", c => c.String());
            AddColumn("dbo.PartModel", "EsteemCodeAlt", c => c.String());
            AddColumn("dbo.PartModel", "Description", c => c.String());
            AddColumn("dbo.PartModel", "FullDescription", c => c.String());
            DropColumn("dbo.PartModel", "Code");
            DropColumn("dbo.PartModel", "CodeEsteem");
            DropColumn("dbo.PartModel", "CodeEsteemAlt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PartModel", "CodeEsteemAlt", c => c.String());
            AddColumn("dbo.PartModel", "CodeEsteem", c => c.String());
            AddColumn("dbo.PartModel", "Code", c => c.String());
            DropColumn("dbo.PartModel", "FullDescription");
            DropColumn("dbo.PartModel", "Description");
            DropColumn("dbo.PartModel", "EsteemCodeAlt");
            DropColumn("dbo.PartModel", "EsteemCode");
        }
    }
}

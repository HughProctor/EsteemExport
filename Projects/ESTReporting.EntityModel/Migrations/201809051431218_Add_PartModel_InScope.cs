namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PartModel_InScope : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartModel", "IsInScope", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PartModel", "IsInScope");
        }
    }
}

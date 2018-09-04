namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ExceptionCountTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceProgressReport", "ExceptionCountTotal", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceProgressReport", "ExceptionCountTotal");
        }
    }
}

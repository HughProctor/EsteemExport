namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ExceptionCountTotal1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceProgressReport", "QueryStartParameters", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ServiceProgressReport", "QueryEndParameters", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ServiceProgressReport", "QueryString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceProgressReport", "QueryString");
            DropColumn("dbo.ServiceProgressReport", "QueryEndParameters");
            DropColumn("dbo.ServiceProgressReport", "QueryStartParameters");
        }
    }
}

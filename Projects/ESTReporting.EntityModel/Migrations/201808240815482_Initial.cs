namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ESTPartDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        EsteemCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ESTPart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        SerialNumber = c.String(),
                        AssetName = c.String(),
                        DisplayName = c.String(),
                        RequestUser = c.String(),
                        CostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartManufacturer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Name = c.String(),
                        Code = c.String(),
                        CodeEsteem = c.String(),
                        CodeEsteemAlt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        Name = c.String(),
                        EsteemCode = c.String(),
                        EsteemCodeAlt = c.String(),
                        Description = c.String(),
                        FullDescription = c.String(),
                        PartManufacturer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartManufacturer", t => t.PartManufacturer_Id)
                .Index(t => t.PartManufacturer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartModel", "PartManufacturer_Id", "dbo.PartManufacturer");
            DropIndex("dbo.PartModel", new[] { "PartManufacturer_Id" });
            DropTable("dbo.PartModel");
            DropTable("dbo.PartManufacturer");
            DropTable("dbo.ESTPart");
            DropTable("dbo.ESTPartDescription");
        }
    }
}

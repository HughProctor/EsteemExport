namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ESTPartDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        EsteemCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ESTParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
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
                "dbo.PartManufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        CodeEsteem = c.String(),
                        CodeEsteemAlt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        CodeEsteem = c.String(),
                        CodeEsteemAlt = c.String(),
                        PartManufacturer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartManufacturers", t => t.PartManufacturer_Id)
                .Index(t => t.PartManufacturer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartModels", "PartManufacturer_Id", "dbo.PartManufacturers");
            DropIndex("dbo.PartModels", new[] { "PartManufacturer_Id" });
            DropTable("dbo.PartModels");
            DropTable("dbo.PartManufacturers");
            DropTable("dbo.ESTParts");
            DropTable("dbo.ESTPartDescriptions");
        }
    }
}

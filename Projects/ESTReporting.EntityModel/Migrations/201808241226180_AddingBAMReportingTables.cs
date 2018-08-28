namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBAMReportingTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BAM_AssetStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        BAM_Id = c.String(),
                        Name = c.String(),
                        HierarchyLevel = c.Int(nullable: false),
                        HierarchyPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BAM_HardwareAssetStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        BAM_Id = c.String(),
                        Name = c.String(),
                        HierarchyLevel = c.Int(nullable: false),
                        HierarchyPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BAM_HardwareAssetType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        BAM_Id = c.String(),
                        Name = c.String(),
                        HierarchyLevel = c.Int(nullable: false),
                        HierarchyPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BAM_HardwareTemplate_Full",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        ClassTypeId = c.String(),
                        BaseId = c.String(),
                        DisplayName = c.String(),
                        FullName = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        AssetTag = c.String(),
                        AssignedDate = c.DateTime(),
                        Cost = c.Single(),
                        HardwareAssetID = c.String(),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        SerialNumber = c.String(),
                        ObjectId = c.String(),
                        AssetStatus_Id = c.Int(),
                        HardwareAssetStatus_Id = c.Int(),
                        HardwareAssetType_Id = c.Int(),
                        ManufacturerEnum_Id = c.Int(),
                        ModelEnum_Id = c.Int(),
                        Target_HardwareAssetHasCostCenter_Id = c.Int(),
                        Target_HardwareAssetHasLocation_Id = c.Int(),
                        Target_HardwareAssetHasPrimaryUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BAM_AssetStatus", t => t.AssetStatus_Id)
                .ForeignKey("dbo.BAM_HardwareAssetStatus", t => t.HardwareAssetStatus_Id)
                .ForeignKey("dbo.BAM_HardwareAssetType", t => t.HardwareAssetType_Id)
                .ForeignKey("dbo.BAM_ManufacturerEnum", t => t.ManufacturerEnum_Id)
                .ForeignKey("dbo.BAM_ModelEnum", t => t.ModelEnum_Id)
                .ForeignKey("dbo.BAM_TargetHardwareAssetHasCostCenter", t => t.Target_HardwareAssetHasCostCenter_Id)
                .ForeignKey("dbo.BAM_TargetHardwareAssetHasLocation", t => t.Target_HardwareAssetHasLocation_Id)
                .ForeignKey("dbo.BAM_TargetHardwareAssetHasPrimaryUser", t => t.Target_HardwareAssetHasPrimaryUser_Id)
                .Index(t => t.AssetStatus_Id)
                .Index(t => t.HardwareAssetStatus_Id)
                .Index(t => t.HardwareAssetType_Id)
                .Index(t => t.ManufacturerEnum_Id)
                .Index(t => t.ModelEnum_Id)
                .Index(t => t.Target_HardwareAssetHasCostCenter_Id)
                .Index(t => t.Target_HardwareAssetHasLocation_Id)
                .Index(t => t.Target_HardwareAssetHasPrimaryUser_Id);
            
            CreateTable(
                "dbo.BAM_ManufacturerEnum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        BAM_Id = c.String(),
                        Name = c.String(),
                        HierarchyLevel = c.Int(nullable: false),
                        HierarchyPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BAM_ModelEnum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        BAM_Id = c.String(),
                        Name = c.String(),
                        HierarchyLevel = c.Int(nullable: false),
                        HierarchyPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BAM_TargetHardwareAssetHasCostCenter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        ClassTypeId = c.String(),
                        BaseId = c.String(),
                        DisplayName = c.String(),
                        FullName = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        AssetStatus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BAM_AssetStatus", t => t.AssetStatus_Id)
                .Index(t => t.AssetStatus_Id);
            
            CreateTable(
                "dbo.BAM_TargetHardwareAssetHasLocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        ClassTypeId = c.String(),
                        BaseId = c.String(),
                        DisplayName = c.String(),
                        FullName = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        AssetStatus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BAM_AssetStatus", t => t.AssetStatus_Id)
                .Index(t => t.AssetStatus_Id);
            
            CreateTable(
                "dbo.BAM_TargetHardwareAssetHasPrimaryUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        ClassTypeId = c.String(),
                        BaseId = c.String(),
                        DisplayName = c.String(),
                        FullName = c.String(),
                        TimeAdded = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        BAMCostCode = c.String(),
                        BAMEmployeeNumber = c.String(),
                        AssetStatus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BAM_AssetStatus", t => t.AssetStatus_Id)
                .Index(t => t.AssetStatus_Id);
            
            CreateTable(
                "dbo.BAM_Reporting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        SCAudit_Ref = c.Int(nullable: false),
                        SCAuditDeploy_Ref = c.Int(nullable: false),
                        BAM_HardwareTemplate_Exception_Ref = c.Int(nullable: false),
                        BAM_HardwareTemplate_Exception_Id = c.Int(),
                        SCAudit_Item_Id = c.Int(),
                        SCAuditDeploy_Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BAM_HardwareTemplate_Full", t => t.BAM_HardwareTemplate_Exception_Id)
                .ForeignKey("dbo.EST_SCAudit", t => t.SCAudit_Item_Id)
                .ForeignKey("dbo.EST_SCAuditDeploy", t => t.SCAuditDeploy_Item_Id)
                .Index(t => t.BAM_HardwareTemplate_Exception_Id)
                .Index(t => t.SCAudit_Item_Id)
                .Index(t => t.SCAuditDeploy_Item_Id);
            
            CreateTable(
                "dbo.EST_SCAudit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Audit_Ser_Num = c.String(),
                        Audit_Part_Num = c.String(),
                        Audit_Prod_Desc = c.String(),
                        Audit_Prod_Desc_Alt = c.String(),
                        Audit_Source_Site_Num = c.String(),
                        Audit_Dest_Site_Num = c.String(),
                        Audit_Part_Type = c.String(),
                        Audit_Rem = c.String(),
                        Audit_User = c.String(),
                        Audit_Last_Update = c.DateTime(nullable: false),
                        Audit_Move_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EST_SCAuditDeploy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Audit_Ser_Num = c.String(),
                        Audit_Ser_Num_Returned = c.String(),
                        Audit_Part_Num = c.String(),
                        Audit_Part_Num_Returned = c.String(),
                        Audit_Prod_Desc = c.String(),
                        Audit_Prod_Desc_Returned = c.String(),
                        Audit_Source_Site_Num = c.String(),
                        Audit_Dest_Site_Num = c.String(),
                        Audit_Part_Type = c.String(),
                        Audit_Part_Code = c.String(),
                        Audit_Rem = c.String(),
                        Audit_User = c.String(),
                        Audit_Last_Update = c.DateTime(nullable: false),
                        Audit_Cost_Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ESTPartDescription", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.ESTPart", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.PartManufacturer", "DeletedDate", c => c.DateTime());
            AddColumn("dbo.PartModel", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BAM_Reporting", "SCAuditDeploy_Item_Id", "dbo.EST_SCAuditDeploy");
            DropForeignKey("dbo.BAM_Reporting", "SCAudit_Item_Id", "dbo.EST_SCAudit");
            DropForeignKey("dbo.BAM_Reporting", "BAM_HardwareTemplate_Exception_Id", "dbo.BAM_HardwareTemplate_Full");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "Target_HardwareAssetHasPrimaryUser_Id", "dbo.BAM_TargetHardwareAssetHasPrimaryUser");
            DropForeignKey("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "AssetStatus_Id", "dbo.BAM_AssetStatus");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "Target_HardwareAssetHasLocation_Id", "dbo.BAM_TargetHardwareAssetHasLocation");
            DropForeignKey("dbo.BAM_TargetHardwareAssetHasLocation", "AssetStatus_Id", "dbo.BAM_AssetStatus");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "Target_HardwareAssetHasCostCenter_Id", "dbo.BAM_TargetHardwareAssetHasCostCenter");
            DropForeignKey("dbo.BAM_TargetHardwareAssetHasCostCenter", "AssetStatus_Id", "dbo.BAM_AssetStatus");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "ModelEnum_Id", "dbo.BAM_ModelEnum");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "ManufacturerEnum_Id", "dbo.BAM_ManufacturerEnum");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "HardwareAssetType_Id", "dbo.BAM_HardwareAssetType");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "HardwareAssetStatus_Id", "dbo.BAM_HardwareAssetStatus");
            DropForeignKey("dbo.BAM_HardwareTemplate_Full", "AssetStatus_Id", "dbo.BAM_AssetStatus");
            DropIndex("dbo.BAM_Reporting", new[] { "SCAuditDeploy_Item_Id" });
            DropIndex("dbo.BAM_Reporting", new[] { "SCAudit_Item_Id" });
            DropIndex("dbo.BAM_Reporting", new[] { "BAM_HardwareTemplate_Exception_Id" });
            DropIndex("dbo.BAM_TargetHardwareAssetHasPrimaryUser", new[] { "AssetStatus_Id" });
            DropIndex("dbo.BAM_TargetHardwareAssetHasLocation", new[] { "AssetStatus_Id" });
            DropIndex("dbo.BAM_TargetHardwareAssetHasCostCenter", new[] { "AssetStatus_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "Target_HardwareAssetHasPrimaryUser_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "Target_HardwareAssetHasLocation_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "Target_HardwareAssetHasCostCenter_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "ModelEnum_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "ManufacturerEnum_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "HardwareAssetType_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "HardwareAssetStatus_Id" });
            DropIndex("dbo.BAM_HardwareTemplate_Full", new[] { "AssetStatus_Id" });
            DropColumn("dbo.PartModel", "DeletedDate");
            DropColumn("dbo.PartManufacturer", "DeletedDate");
            DropColumn("dbo.ESTPart", "DeletedDate");
            DropColumn("dbo.ESTPartDescription", "DeletedDate");
            DropTable("dbo.EST_SCAuditDeploy");
            DropTable("dbo.EST_SCAudit");
            DropTable("dbo.BAM_Reporting");
            DropTable("dbo.BAM_TargetHardwareAssetHasPrimaryUser");
            DropTable("dbo.BAM_TargetHardwareAssetHasLocation");
            DropTable("dbo.BAM_TargetHardwareAssetHasCostCenter");
            DropTable("dbo.BAM_ModelEnum");
            DropTable("dbo.BAM_ManufacturerEnum");
            DropTable("dbo.BAM_HardwareTemplate_Full");
            DropTable("dbo.BAM_HardwareAssetType");
            DropTable("dbo.BAM_HardwareAssetStatus");
            DropTable("dbo.BAM_AssetStatus");
        }
    }
}

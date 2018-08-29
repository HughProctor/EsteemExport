namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTimeNulls3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BAM_AssetStatus", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_AssetStatus", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_AssetStatus", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareAssetStatus", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareAssetStatus", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareAssetStatus", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareAssetType", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareAssetType", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareAssetType", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "TimeAdded", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "LastModified", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "AssignedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_ManufacturerEnum", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_ManufacturerEnum", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_ManufacturerEnum", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_ModelEnum", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_ModelEnum", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_ModelEnum", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "TimeAdded", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "LastModified", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "TimeAdded", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "LastModified", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "TimeAdded", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "LastModified", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_Reporting", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_Reporting", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BAM_Reporting", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAudit", "Audit_Last_Update", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAudit", "Audit_Move_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAudit", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAudit", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAudit", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAuditDeploy", "Audit_Last_Update", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAuditDeploy", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAuditDeploy", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EST_SCAuditDeploy", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ESTPartDescription", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ESTPartDescription", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ESTPartDescription", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ESTPart", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ESTPart", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ESTPart", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PartManufacturer", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PartManufacturer", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PartManufacturer", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PartModel", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PartModel", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PartModel", "DeletedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PartModel", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.PartModel", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.PartModel", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PartManufacturer", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.PartManufacturer", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.PartManufacturer", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ESTPart", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.ESTPart", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.ESTPart", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ESTPartDescription", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.ESTPartDescription", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.ESTPartDescription", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EST_SCAuditDeploy", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.EST_SCAuditDeploy", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.EST_SCAuditDeploy", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EST_SCAuditDeploy", "Audit_Last_Update", c => c.DateTime());
            AlterColumn("dbo.EST_SCAudit", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.EST_SCAudit", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.EST_SCAudit", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EST_SCAudit", "Audit_Move_Date", c => c.DateTime());
            AlterColumn("dbo.EST_SCAudit", "Audit_Last_Update", c => c.DateTime());
            AlterColumn("dbo.BAM_Reporting", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_Reporting", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_Reporting", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "LastModified", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "LastModified", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "LastModified", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_ModelEnum", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_ModelEnum", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_ModelEnum", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_ManufacturerEnum", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_ManufacturerEnum", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_ManufacturerEnum", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "AssignedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "LastModified", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareAssetType", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareAssetType", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareAssetType", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_HardwareAssetStatus", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareAssetStatus", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareAssetStatus", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_AssetStatus", "DeletedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_AssetStatus", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.BAM_AssetStatus", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}

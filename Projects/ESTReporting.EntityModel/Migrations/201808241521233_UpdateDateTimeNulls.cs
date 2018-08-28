namespace ESTReporting.EntityModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTimeNulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "LastModified", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "LastModified", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "LastModified", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "TimeAdded", c => c.DateTime());
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "LastModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "LastModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasPrimaryUser", "TimeAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "LastModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasLocation", "TimeAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "LastModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_TargetHardwareAssetHasCostCenter", "TimeAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "LastModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BAM_HardwareTemplate_Full", "TimeAdded", c => c.DateTime(nullable: false));
        }
    }
}

namespace ServiceModel.Models.BAM.Abstract
{
    public interface IHardwareTemplate_Full
    {
        string BAMAcessoriesCost { get; set; }
        string BAMAverageSpeed { get; set; }
        string BAMBackupConnection { get; set; }
        string BAMDevicePassword { get; set; }
        string BAMDeviceUsername { get; set; }
        string BAMDirectInternet { get; set; }
        string BAMExcessCosts { get; set; }
        bool BAMFollowUser { get; set; }
        string BAMGreenSky { get; set; }
        string BAMIMEINumber { get; set; }
        string BAMIPAddress { get; set; }
        string BAMLineSpeed { get; set; }
        double BAMMonthlyCharge { get; set; }
        string BAMPhoneLine { get; set; }
        string BAMPhoneSystem { get; set; }
        string BAMSIMNumber { get; set; }
        string BAMWhiteSky { get; set; }
        OwnedBy OwnedBy { get; set; }
        TargetHardwareAssetHasAssociatedCI Target_HardwareAssetHasAssociatedCI { get; set; }
        TargetHardwareAssetHasCostCenter Target_HardwareAssetHasCostCenter { get; set; }
        TargetHardwareAssetHasLocation Target_HardwareAssetHasLocation { get; set; }
        TargetHardwareAssetHasPrimaryUser Target_HardwareAssetHasPrimaryUser { get; set; }
    }
}
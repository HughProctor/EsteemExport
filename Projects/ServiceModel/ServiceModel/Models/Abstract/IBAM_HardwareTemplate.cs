using System;

namespace ServiceModel.Models.BAM.Abstract
{
    public interface IHardwareTemplate
    {
        AssetStatus AssetStatus { get; set; }
        string AssetTag { get; set; }
        DateTime? AssignedDate { get; set; }
        string BaseId { get; set; }
        string ClassName { get; set; }
        string ClassTypeId { get; set; }
        float? Cost { get; set; }
        Currency Currency { get; set; }
        string Description { get; set; }
        string DisplayName { get; set; }
        DateTime? DisposalDate { get; set; }
        string DisposalReference { get; set; }
        DateTime? ExpectedDate { get; set; }
        DateTime? ExpectedEndDate { get; set; }
        string FullClassName { get; set; }
        string FullName { get; set; }
        string HardwareAssetID { get; set; }
        HardwareAssetStatus HardwareAssetStatus { get; set; }
        HardwareAssetType HardwareAssetType { get; set; }
        DateTime? LastModified { get; set; }
        string LastModifiedBy { get; set; }
        DateTime? LoanedDate { get; set; }
        DateTime? LoanReturnedDate { get; set; }
        string LocationDetails { get; set; }
        DateTime? ManualInventoryDate { get; set; }
        string Manufacturer { get; set; }
        ManufacturerEnum ManufacturerEnum { get; set; }
        DateTime? MasterContractEndDate { get; set; }
        DateTime? MasterContractRenewedOn { get; set; }
        DateTime? MasterContractStartingDate { get; set; }
        MasterContractStatus MasterContractStatus { get; set; }
        string Model { get; set; }
        ModelEnum ModelEnum { get; set; }
        string Name { get; set; }
        string Notes { get; set; }
        string ObjectId { get; set; }
        ObjectStatus ObjectStatus { get; set; }
        string Path { get; set; }
        DateTime? ReceivedDate { get; set; }
        string SerialNumber { get; set; }
        DateTime? TimeAdded { get; set; }
    }
}
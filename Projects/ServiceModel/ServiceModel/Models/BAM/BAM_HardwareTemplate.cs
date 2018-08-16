using System;
using System.Collections.Generic;

namespace ServiceModel.Models.BAM
{
    public class HardwareTemplateList
    {
        public List<BAM_HardwareTemplate> BAM_HardwareTemplates { get; set; }
    }

    public class AssetStatus
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class Currency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class HardwareAssetStatus
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class HardwareAssetType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class ManufacturerEnum
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class MasterContractStatus
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class ModelEnum
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class ObjectStatus
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }

    public class NameRelationship
    {
        public string Name { get; set; }
        public string RelationshipId { get; set; }
    }

    public class BAM_HardwareTemplate
    {
        public string ClassTypeId { get; set; }
        public string BaseId { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string Path { get; set; }
        public string ClassName { get; set; }
        public string FullClassName { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public AssetStatus AssetStatus { get; set; }
        public string AssetTag { get; set; }
        public DateTime? AssignedDate { get; set; }
        public float? Cost { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public DateTime? DisposalDate { get; set; }
        public string DisposalReference { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public string HardwareAssetID { get; set; }
        public HardwareAssetStatus HardwareAssetStatus { get; set; }
        public HardwareAssetType HardwareAssetType { get; set; }
        public DateTime? LoanedDate { get; set; }
        public DateTime? LoanReturnedDate { get; set; }
        public string LocationDetails { get; set; }
        public DateTime? ManualInventoryDate { get; set; }
        public string Manufacturer { get; set; }
        public ManufacturerEnum ManufacturerEnum { get; set; }
        public DateTime? MasterContractEndDate { get; set; }
        public DateTime? MasterContractRenewedOn { get; set; }
        public DateTime? MasterContractStartingDate { get; set; }
        public MasterContractStatus MasterContractStatus { get; set; }
        public string Model { get; set; }
        public ModelEnum ModelEnum { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public ObjectStatus ObjectStatus { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string SerialNumber { get; set; }
        public string ObjectId { get; set; }
        public List<NameRelationship> NameRelationship { get; set; }
    }
}

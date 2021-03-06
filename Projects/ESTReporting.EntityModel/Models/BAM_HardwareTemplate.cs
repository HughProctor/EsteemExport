﻿using System;

namespace ESTReporting.EntityModel.Models
{
    //public class HardwareTemplateList
    //{
    //    public List<BAM_HardwareTemplate> BAM_HardwareTemplates { get; set; }
    //}

    public class BAM_AssetStatus : BAM_Object
    {
    }

    //public class Currency : BAM_Object
    //{
    //}

    public class BAM_HardwareAssetStatus : BAM_Object
    {
    }

    public class BAM_HardwareAssetType : BAM_Object
    {
    }

    public class BAM_ManufacturerEnum : BAM_Object
    {
    }

    //public class MasterContractStatus : BAM_Object
    //{
    //}

    public class BAM_ModelEnum : BAM_Object
    {
    }

    //public class ObjectStatus : BAM_Object
    //{
    //}

    //public class NameRelationship
    //{
    //    public string Name { get; set; }
    //    public string RelationshipId { get; set; }
    //}

    public class BAM_HardwareTemplate : BaseObjectProperties
    {
        public string ClassTypeId { get; set; }
        public string BaseId { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        //public string Path { get; set; }
        //public string ClassName { get; set; }
        //public string FullClassName { get; set; }
        public DateTime? TimeAdded { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public BAM_AssetStatus AssetStatus { get; set; }
        public string AssetTag { get; set; }
        public DateTime? AssignedDate { get; set; }
        public float? Cost { get; set; }
        //public Currency Currency { get; set; }
        //public string Description { get; set; }
        //public DateTime? DisposalDate { get; set; }
        //public string DisposalReference { get; set; }
        //public DateTime? ExpectedDate { get; set; }
        //public DateTime? ExpectedEndDate { get; set; }
        public string HardwareAssetID { get; set; }
        public BAM_HardwareAssetStatus HardwareAssetStatus { get; set; }
        public BAM_HardwareAssetType HardwareAssetType { get; set; }
        //public DateTime? LoanedDate { get; set; }
        //public DateTime? LoanReturnedDate { get; set; }
        //public string LocationDetails { get; set; }
        //public DateTime? ManualInventoryDate { get; set; }
        public string Manufacturer { get; set; }
        public BAM_ManufacturerEnum ManufacturerEnum { get; set; }
        //public DateTime? MasterContractEndDate { get; set; }
        //public DateTime? MasterContractRenewedOn { get; set; }
        //public DateTime? MasterContractStartingDate { get; set; }
        //public MasterContractStatus MasterContractStatus { get; set; }
        public string Model { get; set; }
        public BAM_ModelEnum ModelEnum { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        //public ObjectStatus ObjectStatus { get; set; }
        //public DateTime? ReceivedDate { get; set; }
        public string SerialNumber { get; set; }
        public string ObjectId { get; set; }
        //public List<NameRelationship> NameRelationship { get; set; }
    }
}

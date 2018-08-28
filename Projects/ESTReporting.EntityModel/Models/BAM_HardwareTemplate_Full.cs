using System;
using System.Collections.Generic;

namespace ESTReporting.EntityModel.Models
{
    //public class HardwareTemplate_FullList
    //{
    //    public List<BAM_HardwareTemplate_Full> BAM_HardwareTemplate_FullList { get; set; }
    //}

    public class BAM_TargetHardwareAssetHasPrimaryUser : BaseObjectProperties
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
        public string BAMCostCode { get; set; }
        public string BAMEmployeeNumber { get; set; }
        //public bool BAMMultiPC { get; set; }
        //public string BusinessPhone { get; set; }
        //public string BusinessPhone2 { get; set; }
        //public string City { get; set; }
        //public string Company { get; set; }
        //public string Country { get; set; }
        //public string Department { get; set; }
        //public string DistinguishedName { get; set; }
        //public string Domain { get; set; }
        //public string EmployeeId { get; set; }
        //public string Fax { get; set; }
        //public string FirstName { get; set; }
        //public string FQDN { get; set; }
        //public string HomePhone { get; set; }
        //public string HomePhone2 { get; set; }
        //public string Initials { get; set; }
        //public string LastName { get; set; }
        //public string Mobile { get; set; }
        //public string Notes { get; set; }
        //public string stringGuid { get; set; }
        //public ObjectStatus ObjectStatus { get; set; }
        //public string Office { get; set; }
        //public string OrganizationalUnit { get; set; }
        //public string Pager { get; set; }
        //public string SID { get; set; }
        //public string State { get; set; }
        //public string StreetAddress { get; set; }
        //public string Title { get; set; }
        //public string UPN { get; set; }
        //public string UserName { get; set; }
        //public string Zip { get; set; }
    }

    //public class OwnedBy
    //{
    //    public int Id { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime? UpdatedDate { get; set; }
    //    public string ClassTypeId { get; set; }
    //    public string BaseId { get; set; }
    //    public string DisplayName { get; set; }
    //    public string FullName { get; set; }
    //    public string Path { get; set; }
    //    public string ClassName { get; set; }
    //    public string FullClassName { get; set; }
    //    public DateTime TimeAdded { get; set; }
    //    public DateTime LastModified { get; set; }
    //    public string LastModifiedBy { get; set; }
    //    public AssetStatus AssetStatus { get; set; }
    //    public string BAMCostCode { get; set; }
    //    public string BAMEmployeeNumber { get; set; }
    //    public bool BAMMultiPC { get; set; }
    //    public string BusinessPhone { get; set; }
    //    public string BusinessPhone2 { get; set; }
    //    public string City { get; set; }
    //    public string Company { get; set; }
    //    public string Country { get; set; }
    //    public string Department { get; set; }
    //    public string DistinguishedName { get; set; }
    //    public string Domain { get; set; }
    //    public string EmployeeId { get; set; }
    //    public string Fax { get; set; }
    //    public string FirstName { get; set; }
    //    public string FQDN { get; set; }
    //    public string HomePhone { get; set; }
    //    public string HomePhone2 { get; set; }
    //    public string Initials { get; set; }
    //    public string LastName { get; set; }
    //    public string Mobile { get; set; }
    //    public string Notes { get; set; }
    //    public string stringGuid { get; set; }
    //    public ObjectStatus ObjectStatus { get; set; }
    //    public string Office { get; set; }
    //    public string OrganizationalUnit { get; set; }
    //    public string Pager { get; set; }
    //    public string SID { get; set; }
    //    public string State { get; set; }
    //    public string StreetAddress { get; set; }
    //    public string Title { get; set; }
    //    public string UPN { get; set; }
    //    public string UserName { get; set; }
    //    public string Zip { get; set; }
    //}

    //public class TargetHardwareAssetHasAssociatedCI
    //{
    //    public int Id { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime? UpdatedDate { get; set; }
    //    public string ClassTypeId { get; set; }
    //    public string BaseId { get; set; }
    //    public string DisplayName { get; set; }
    //    public string FullName { get; set; }
    //    public string Path { get; set; }
    //    public string ClassName { get; set; }
    //    public string FullClassName { get; set; }
    //    public DateTime TimeAdded { get; set; }
    //    public DateTime LastModified { get; set; }
    //    public string LastModifiedBy { get; set; }
    //    public string ActiveDirectorystringSid { get; set; }
    //    public string ActiveDirectorySite { get; set; }
    //    public AssetStatus AssetStatus { get; set; }
    //    public string DNSName { get; set; }
    //    public string DomainDnsName { get; set; }
    //    public string ForestDnsName { get; set; }
    //    public string HostServerName { get; set; }
    //    public string IPAddress { get; set; }
    //    public bool IsVirtualMachine { get; set; }
    //    public DateTime LastInventoryDate { get; set; }
    //    public string LogicalProcessors { get; set; }
    //    public string NetbiosComputerName { get; set; }
    //    public string NetbiosDomainName { get; set; }
    //    public string NetworkName { get; set; }
    //    public string Notes { get; set; }
    //    public ObjectStatus ObjectStatus { get; set; }
    //    public int OffsetInMinuteFromGreenwichTime { get; set; }
    //    public string OrganizationalUnit { get; set; }
    //    public string PhysicalProcessors { get; set; }
    //    public string PrincipalName { get; set; }
    //    public string VirtualMachineName { get; set; }
    //}

    public class BAM_TargetHardwareAssetHasCostCenter : BaseObjectProperties
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
        public string Description { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        //public ObjectStatus ObjectStatus { get; set; }
        //public string Id { get; set; }
    }

    public class BAM_TargetHardwareAssetHasLocation : BaseObjectProperties
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
        //public string LocationAddress1 { get; set; }
        //public string LocationAddress2 { get; set; }
        //public string LocationCity { get; set; }
        //public string LocationContact { get; set; }
        //public string LocationCountry { get; set; }
        //public string LocationEmail { get; set; }
        //public string LocationFax { get; set; }
        //public string LocationPhone { get; set; }
        //public string LocationPostCode { get; set; }
        //public string LocationState { get; set; }
        //public string LocationTaxRate { get; set; }
        //public string Name { get; set; }
        //public string Notes { get; set; }
        //public ObjectStatus ObjectStatus { get; set; }
        //public string Id { get; set; }
    }

    public class BAM_HardwareTemplate_Full : BAM_HardwareTemplate
    {
        public BAM_TargetHardwareAssetHasPrimaryUser Target_HardwareAssetHasPrimaryUser { get; set; }
        //public OwnedBy OwnedBy { get; set; }
        //public TargetHardwareAssetHasAssociatedCI Target_HardwareAssetHasAssociatedCI { get; set; }
        public BAM_TargetHardwareAssetHasCostCenter Target_HardwareAssetHasCostCenter { get; set; }
        public BAM_TargetHardwareAssetHasLocation Target_HardwareAssetHasLocation { get; set; }
        //public string BAMAcessoriesCost { get; set; }
        //public string BAMAverageSpeed { get; set; }
        //public string BAMBackupConnection { get; set; }
        //public string BAMDevicePassword { get; set; }
        //public string BAMDeviceUsername { get; set; }
        //public string BAMDirectInternet { get; set; }
        //public string BAMExcessCosts { get; set; }
        //public bool BAMFollowUser { get; set; }
        //public string BAMGreenSky { get; set; }
        //public string BAMIMEINumber { get; set; }
        //public string BAMIPAddress { get; set; }
        //public string BAMLineSpeed { get; set; }
        //public double BAMMonthlyCharge { get; set; }
        //public string BAMPhoneLine { get; set; }
        //public string BAMPhoneSystem { get; set; }
        //public string BAMSIMNumber { get; set; }
        //public string BAMWhiteSky { get; set; }
    }
}
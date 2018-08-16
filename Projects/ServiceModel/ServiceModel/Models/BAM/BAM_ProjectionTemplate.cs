using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ServiceModel
{
    public class BAM_ProjectionTemplate
    {
        public int TemplateId { get; set; }
        public Guid CreatedById { get; set; }
    }

    public class Projection
    {
        public int ObjectId { get; set; }
        public string DisplayName { get; set; }
        public string AssetName { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string HWAssetStatus { get; set; }
        [JsonIgnore]
        public BAM_HWAssetStatus HWAssetStatus_Enum { get; set; }
        public string ObjectStatus { get; set; }

    }

    public enum BAM_HWAssetStatus
    {
        [StringValue("Purchase Order")]
        [Description("Purchase Order")]
        PurchaseOrder = 1,
        [StringValue("New Item")]
        [Description("In Stock")]
        NewItem = 2,
        [StringValue("Commissioned")]
        [Description("Commissioned")]
        Commissioned = 3,
        [StringValue("In Stock")]
        [Description("In Stock")]
        Stocked = 4,
        [StringValue("Deployed")]
        [Description("Deployed")]
        Deployed = 5,
        [StringValue("Returned")]
        [Description("Returned")]
        Returned = 6,
        [StringValue("Retired")]
        [Description("Retired")]
        Retired = 7,
        [StringValue("Ammended")]
        [Description("Ammended")]
        Ammended = 8,
        [StringValue("Disposed")]
        [Description("Disposed")]
        Disposed = 9

    }
}

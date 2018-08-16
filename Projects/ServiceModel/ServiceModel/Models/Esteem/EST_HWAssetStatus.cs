using System.ComponentModel;

namespace ServiceModel.Models.Esteem
{
    public enum EST_HWAssetStatus
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

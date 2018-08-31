using System.ComponentModel;

namespace ESTReporting.EntityModel.Models
{
    public enum EST_HWAssetStatus
    {
        [Description("Purchase Order")]
        PurchaseOrder = 1,
        [Description("In Stock")]
        NewItem = 2,
        [Description("Commissioned")]
        Commissioned = 3,
        [Description("In Stock")]
        Stocked = 4,
        [Description("Deployed")]
        Deployed = 5,
        [Description("Returned")]
        Returned = 6,
        [Description("Retired")]
        Retired = 7,
        [Description("Ammended")]
        Ammended = 8,
        [Description("Disposed")]
        Disposed = 9,
        [Description("Location Changed")]
        LocationChanged = 10,
        [Description("Asset Tag Changed")]
        AssetTagChanged = 11
    }
}

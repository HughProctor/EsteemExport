using System.ComponentModel;

namespace ServiceModel.Models.Esteem
{
    public enum EST_HWAssetStatus
    {
        [BAMStringValue("In Stock")]
        [Description("Purchase Order")]
        PurchaseOrder = 1,
        [BAMStringValue("In Stock")]
        [Description("In Stock")]
        NewItem = 2,
        [BAMStringValue("Commissioned")]
        [Description("Commissioned")]
        Commissioned = 3,
        [BAMStringValue("In Stock")]
        [Description("In Stock")]
        Stocked = 4,
        [BAMStringValue("Deployed")]
        [Description("Deployed")]
        Deployed = 5,
        [BAMStringValue("In Stock")]
        [Description("Returned")]
        Returned = 6,
        [BAMStringValue("Retired")]
        [Description("Retired")]
        Retired = 7,
        [BAMStringValue("In Stock")]
        [Description("Ammended")]
        Ammended = 8,
        [BAMStringValue("Disposed")]
        [Description("Disposed")]
        Disposed = 9,
        [BAMStringValue("NA")]
        [Description("Location Changed")]
        LocationChanged = 10,
        [BAMStringValue("NA")]
        [Description("Asset Tag Changed")]
        AssetTagChanged = 11
    }
}

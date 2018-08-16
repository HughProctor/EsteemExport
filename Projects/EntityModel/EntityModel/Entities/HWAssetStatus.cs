using System.ComponentModel;

namespace EntityModel.Entities
{
    public enum HWAssetStatus
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
        Disposed = 9
    }
}

using System.Collections.Generic;

namespace EntityModel
{
    public interface ISCBaseObject
    {
        string AssetName { get; }
        string DisplayName { get; }
        string Manufacturer { get; }
        string Model { get; set; }
        string RequestUser { get; }
        string SerialNumber { get; }
    }
}
using System.Collections.Generic;

namespace BusinessModel.Models
{
    public class SCBaseObject : BaseObjectProperties
    {
        public string AssetName { get; set; }
        public string DisplayName { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string RequestUser { get; set; }
        public string SerialNumber { get; set; }
    }
}
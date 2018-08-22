using System;

namespace ESTReporting.EntityModel.Models
{
    public class ESTPart
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string AssetName { get; set; }
        public string DisplayName { get; set; }
        public string RequestUser { get; set; }
        public string CostCode { get; set; }
    }
}

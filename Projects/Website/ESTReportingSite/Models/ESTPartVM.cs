using System;

namespace ESTReportingSite.Models
{
    public class ESTPartVM
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string AssetName { get; set; }
        public string DisplayName { get; set; }
        public string RequestUser { get; set; }
        public string CostCode { get; set; }
    }
}

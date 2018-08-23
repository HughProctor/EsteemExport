using EntityModel.Entities;

namespace BusinessModel.Models
{
    public class BAMDataModel 
    {
        public string AssetName { get; }
        public string DisplayName { get; }
        public string Manufacturer { get; }
        public string Model { get; set; }
        public string RequestUser { get; }
        public string SerialNumber { get; }
        public HWAssetStatus HWAssetStatus { get; set; }
        public string CostCode { get; set; }
    }
}

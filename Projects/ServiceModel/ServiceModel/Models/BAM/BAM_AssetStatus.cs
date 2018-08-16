using System.Collections.Generic;

namespace ServiceModel.Models.BAM
{
    public class EnumNode
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
    }

    public class BAM_AssetStatusList
    {
        public List<AssetStatus> AssetStatuses { get; set; }
        public List<HardwareAssetStatus> HardwareAssetStatuses { get; set; }
    }
}

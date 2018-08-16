using System.Collections.Generic;

namespace ServiceModel.Models.BAM
{
    public class EnumNode
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
    }

    public class BAM_AssetStatus
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
        public double Ordinal { get; set; }
        public List<EnumNode> EnumNodes { get; set; }
    }

    public class BAM_AssetStatusList
    {
        public List<BAM_AssetStatus> BAM_AssetStatuses { get; set; }
    }
}

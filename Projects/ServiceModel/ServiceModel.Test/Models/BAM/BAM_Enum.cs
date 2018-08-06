using System.Collections.Generic;

namespace ServiceModel.Test.Models.BAM
{
    public class EnumNode
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
    }

    public class BAM_Enum
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
        public double Ordinal { get; set; }
        public List<EnumNode> EnumNodes { get; set; }
    }

    public class BAM_EnumList
    {
        public List<BAM_Enum> BAM_Enums { get; set; }
    }
}

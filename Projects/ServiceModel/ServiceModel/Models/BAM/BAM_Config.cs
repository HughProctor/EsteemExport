using System.Collections.Generic;

namespace ServiceModel.Models.BAM
{
    public class ConfigList
    {
        public List<Config> Configs { get; set; }
    }

    public class Config
    {
        public string m_Item1 { get; set; }
        public string m_Item2 { get; set; }
    }
}

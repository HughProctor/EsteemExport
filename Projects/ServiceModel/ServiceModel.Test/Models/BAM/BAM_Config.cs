using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Test.Models.BAM
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

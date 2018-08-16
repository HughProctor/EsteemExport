using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Models.BAM
{
    public class BAM_Object
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }
}

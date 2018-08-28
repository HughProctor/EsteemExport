using System;

namespace ESTReporting.EntityModel.Models
{
    public class BAM_Object : BaseObjectProperties
    {
        public string BAM_Id { get; set; }
        public string Name { get; set; }
        public int HierarchyLevel { get; set; }
        public string HierarchyPath { get; set; }
    }
}

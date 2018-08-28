using System;

namespace ESTReporting.EntityModel.Models
{
    public class PartModel : BaseObjectProperties
    {
        public string Name { get; set; }
        public string EsteemCode { get; set; }
        public string EsteemCodeAlt { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
    }
}

using System;

namespace ESTReporting.EntityModel.Models
{
    public class ESTPartDescription : BaseObjectProperties
    {
        public string EsteemCode { get; set; }
        public object Description { get; set; }
        public object FullDescription { get; set; }
    }
}

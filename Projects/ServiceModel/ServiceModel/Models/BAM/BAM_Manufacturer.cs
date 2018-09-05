using System;

namespace ServiceModel.Models.BAM
{
    public class BAM_Manufacturer
    {
        public string Id { get; set; }
        public string BaseId { get; set; }
        public string DisplayName { get; set; }
        public string ClassName { get; set; }
        public string FullClassName { get; set; }
        //public object Path { get; set; }
        public DateTime LastModified { get; set; }
        public string Name { get; set; }
        public string ModelString { get; set; }
        public string ManufacturerString { get; set; }
    }
}

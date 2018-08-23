using System;

namespace ESTReporting.EntityModel.Models
{
    public class PartModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public string EsteemCode { get; set; }
        public string EsteemCodeAlt { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
    }
}

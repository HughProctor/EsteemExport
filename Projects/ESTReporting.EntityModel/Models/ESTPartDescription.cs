using System;

namespace ESTReporting.EntityModel.Models
{
    public class ESTPartDescription
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string EsteemCode { get; set; }
        public object Description { get; set; }
        public object FullDescription { get; set; }
    }
}

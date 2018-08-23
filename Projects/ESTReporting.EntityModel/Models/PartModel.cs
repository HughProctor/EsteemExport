using System;

namespace ESTReporting.EntityModel.Models
{
    public class PartModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeEsteem { get; set; }
        public string CodeEsteemAlt { get; set; }
    }
}

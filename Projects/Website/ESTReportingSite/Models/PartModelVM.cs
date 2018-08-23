using System;

namespace ESTReportingSite.Models
{
    public class PartModelVM
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeEsteem { get; set; }
        public string CodeEsteemAlt { get; set; }
    }
}

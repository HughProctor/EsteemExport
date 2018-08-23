using System;
using System.Collections.Generic;

namespace ESTReportingSite.Models
{
    public class PartManufacturerVM
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeEsteem { get; set; }
        public string CodeEsteemAlt { get; set; }
        public virtual List<PartModelVM> PartModels { get; set; }
    }
}
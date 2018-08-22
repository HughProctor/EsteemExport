using System.Collections.Generic;

namespace ESTReportingSite.Models
{
    public class PartManufacturer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CodeEsteem { get; set; }
        public string CodeEsteemAlt { get; set; }
        public virtual List<PartModel> PartModels { get; set; }
    }
}
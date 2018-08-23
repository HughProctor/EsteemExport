using System;

namespace ESTReportingSite.Models
{
    public class ESTPartDescriptionVM
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string EsteemCode { get; internal set; }
        public object Description { get; internal set; }
        public object FullDescription { get; internal set; }
    }
}

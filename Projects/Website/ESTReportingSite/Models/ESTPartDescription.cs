namespace ESTReportingSite.Models
{
    public class ESTPartDescription
    {
        public string Id { get; set; }
        public string EsteemCode { get; internal set; }
        public object Description { get; internal set; }
        public object FullDescription { get; internal set; }
    }
}

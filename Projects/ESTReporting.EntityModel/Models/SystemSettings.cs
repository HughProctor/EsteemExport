namespace ESTReporting.EntityModel.Models
{
    public class SystemSetting : BaseObjectProperties
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int SortOrder { get; set; }
        public string Parameter01 { get; set; }
        public string Parameter02 { get; set; }
    }
}

namespace BusinessModel.Models
{
    public class EST_BAM_ModelLookup : BaseObjectProperties
    {
        public string EST_ManufacturerCode { get; set; }
        public string EST_ModelDescription { get; set; }
        public string BAM_Name { get; set; }
        public string BAM_ModelString { get; set; }
        public string BAM_ManufacturerString { get; set; }
        public string BAM_DisplayName { get; set; }
        public int IsActive { get; set; }
    }
}

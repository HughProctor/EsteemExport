using System;

namespace ESTReporting.EntityModel.Models
{
    public class EST_SCAudit : BaseObjectProperties
    {
        public string Audit_Ser_Num { get; set; }
        public string Audit_Part_Num { get; set; }
        public string Audit_Prod_Desc { get; set; }
        public string Audit_Prod_Desc_Alt { get; set; }
        public string Audit_Source_Site_Num { get; set; }
        public string Audit_Dest_Site_Num { get; set; }
        public string Audit_Part_Type { get; set; }
        public string Audit_Rem { get; set; }
        public string Audit_User { get; set; }
        public DateTime? Audit_Last_Update { get; set; }
        public DateTime? Audit_Move_Date { get; set; }
        public string AssetName { get; set; }
        public string DisplayName { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string RequestUser { get; set; }
        public string SerialNumber { get; set; }
    }
}

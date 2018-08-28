using System;

namespace ESTReporting.EntityModel.Models
{
    public class EST_SCAuditDeploy : BaseObjectProperties
    {
        public string Audit_Ser_Num { get; set; }
        public string Audit_Ser_Num_Returned { get; set; }
        public string Audit_Part_Num { get; set; }
        public string Audit_Part_Num_Returned { get; set; }
        public string Audit_Prod_Desc { get; set; }
        public string Audit_Prod_Desc_Returned { get; set; }
        public string Audit_Source_Site_Num { get; set; } 
        public string Audit_Dest_Site_Num { get; set; }
        public string Audit_Part_Type { get; set; }
        public string Audit_Part_Code { get; set; }
        public string Audit_Rem { get; set; }
        public string Audit_User { get; set; }
        public DateTime Audit_Last_Update { get; set; }
        public string Audit_Cost_Code { get; set; }
    }
}
